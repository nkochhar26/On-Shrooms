using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Slicer : MonoBehaviour
{
    Vector2 point1, point2;
    public void SetPoint1()
    {
        print("set 1");
        point1 = Input.mousePosition;
    }
    public void SetPoint2()
    {
        print("set 2");
        point2 = Input.mousePosition;
        List<Transform> childList = new List<Transform>();
        foreach(Transform child in transform)
        {
            childList.Add(child);
        }
        foreach(Transform child in childList)
        {
            RectTransform rt = child as RectTransform;

            Vector2 localP1, localP2;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                rt,
                point1,
                null, // camera = null for Screen Space Overlay
                out localP1
            );

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                rt,
                point2,
                null,
                out localP2
            );

            var mf = child.GetComponent<MeshFilter>();
            Mesh mesh1, mesh2;

            Vector3 s = rt.localScale;

Vector2 meshP1 = new Vector2(
    localP1.x / s.x,
    localP1.y / s.y
);

Vector2 meshP2 = new Vector2(
    localP2.x / s.x,
    localP2.y / s.y
);

Cut(GetOutline(mf.mesh), meshP1, meshP2, out mesh1, out mesh2);


            if (mesh1.vertexCount >= 3 && mesh2.vertexCount >= 3)
            {
                MeshFilter child1 = mf;
                MeshFilter child2 = Instantiate(child.gameObject, transform).GetComponent<MeshFilter>();

                child1.mesh = mesh1;
                child2.mesh = mesh2;
            }
        }
    }

    public static List<Vector2> GetOutline(Mesh m)
{
    var v = m.vertices;
    var t = m.triangles;

    var edges = new Dictionary<(int,int), int>();

    void Add(int a, int b)
    {
        if (a > b) (a, b) = (b, a);
        edges[(a,b)] = edges.TryGetValue((a,b), out int c) ? c + 1 : 1;
    }

    for (int i = 0; i < t.Length; i += 3)
    {
        Add(t[i], t[i+1]);
        Add(t[i+1], t[i+2]);
        Add(t[i+2], t[i]);
    }

    // build adjacency
    var adj = new Dictionary<int, List<int>>();
    foreach (var e in edges)
    {
        if (e.Value != 1) continue;

        int a = e.Key.Item1;
        int b = e.Key.Item2;

        if (!adj.ContainsKey(a)) adj[a] = new List<int>();
        if (!adj.ContainsKey(b)) adj[b] = new List<int>();

        adj[a].Add(b);
        adj[b].Add(a);
    }

    // walk loop
    var outline = new List<Vector2>();
    int start = -1;
    foreach (var k in adj.Keys) { start = k; break; }

    int prev = -1;
    int curr = start;

    do
    {
        outline.Add(v[curr]);

        var neighbors = adj[curr];
        int next = neighbors[0] == prev && neighbors.Count > 1
            ? neighbors[1]
            : neighbors[0];

        prev = curr;
        curr = next;

    } while (curr != start && outline.Count < adj.Count + 1);

    return outline;
}


    public static void Cut(
        List<Vector2> poly,
        Vector2 p0,
        Vector2 p1,
        out Mesh a,
        out Mesh b)
    {
        Vector2 d = (p1 - p0).normalized;
        List<Vector2> L = new(), R = new();

        for (int i = 0; i < poly.Count; i++)
        {
            Vector2 A = poly[i];
            Vector2 B = poly[(i + 1) % poly.Count];

            float da = Side(A), db = Side(B);

            if (da >= 0) L.Add(A); else R.Add(A);

            if (da * db < 0)
            {
                Vector2 I = Vector2.Lerp(A, B, da / (da - db));
                L.Add(I); R.Add(I);
            }
        }

        a = Tri(L);
        b = Tri(R);

        float Side(Vector2 p) =>
            (p.x - p0.x) * d.y - (p.y - p0.y) * d.x;
    }

    static Mesh Tri(List<Vector2> p)
    {
        Mesh m = new Mesh();
        if (p.Count < 3) return m;

        Vector3[] v = new Vector3[p.Count];
        int[] t = new int[(p.Count - 2) * 3];

        for (int i = 0; i < p.Count; i++)
            v[i] = p[i];

        for (int i = 0, k = 0; i < p.Count - 2; i++)
        {
            t[k++] = 0;
            t[k++] = i + 1;
            t[k++] = i + 2;
        }

        m.vertices = v;
        m.triangles = t;
        m.RecalculateNormals();
        m.RecalculateBounds();
        return m;
    }
}
