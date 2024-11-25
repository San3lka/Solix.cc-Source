// https://github.com/aerodakdev && https://github.com/San3lka
// Current=> kaka.SilentSphere
// <3

using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#nullable disable
namespace kaka
{
  public static class SilentSphere
  {
    public static bool GetRaycast(GameObject Target, Vector3 StartPos, out Vector3 Point)
    {
      Point = Vector3.zero;
      bool raycast;
      if (Object.op_Equality((Object) Target, (Object) null))
      {
        raycast = false;
      }
      else
      {
        int layer = Target.layer;
        Target.layer = 24;
        SilentComponent Component = Target.GetComponent<SilentComponent>();
        if (VectorUtilities.GetDistance(Target.transform.position, StartPos) <= 15.5)
        {
          Point = ((Component) Player.player).transform.position;
          raycast = true;
        }
        else
        {
          foreach (Vector3 point in ((IEnumerable<Vector3>) Component.Sphere.GetComponent<MeshCollider>().sharedMesh.vertices).Select<Vector3, Vector3>((Func<Vector3, Vector3>) (v => Component.Sphere.transform.TransformPoint(v))).ToArray<Vector3>())
          {
            Vector3 vector3 = VectorUtilities.Normalize(Vector3.op_Subtraction(point, StartPos));
            double distance = VectorUtilities.GetDistance(StartPos, point);
            if (!Physics.Raycast(StartPos, vector3, (float) distance + 0.5f, RayMasks.DAMAGE_CLIENT))
            {
              Target.layer = layer;
              Point = point;
              return true;
            }
          }
          Target.layer = layer;
          raycast = false;
        }
      }
      return raycast;
    }

    public static int GetMiddlePoint(
      int p1,
      int p2,
      ref List<Vector3> vertices,
      ref Dictionary<long, int> cache,
      float radius)
    {
      int num1 = p1 < p2 ? 1 : 0;
      long key = ((num1 != 0 ? (long) p1 : (long) p2) << 32) + (num1 != 0 ? (long) p2 : (long) p1);
      int num2;
      int middlePoint;
      if (cache.TryGetValue(key, out num2))
      {
        middlePoint = num2;
      }
      else
      {
        Vector3 vector3_1 = vertices[p1];
        Vector3 vector3_2 = vertices[p2];
        Vector3 vector3_3;
        // ISSUE: explicit constructor call
        ((Vector3) ref vector3_3).\u002Ector((float) (((double) vector3_1.x + (double) vector3_2.x) / 2.0), (float) (((double) vector3_1.y + (double) vector3_2.y) / 2.0), (float) (((double) vector3_1.z + (double) vector3_2.z) / 2.0));
        int count = vertices.Count;
        vertices.Add(Vector3.op_Multiply(((Vector3) ref vector3_3).normalized, radius));
        cache.Add(key, count);
        middlePoint = count;
      }
      return middlePoint;
    }

    public static GameObject Create(string name, float radius, float recursionLevel)
    {
      GameObject gameObject = new GameObject(name);
      Mesh mesh1 = new Mesh();
      ((Object) mesh1).name = name;
      Mesh mesh2 = mesh1;
      List<Vector3> vertices1 = new List<Vector3>();
      Dictionary<long, int> cache = new Dictionary<long, int>();
      float num = (float) ((1.0 + (double) Mathf.Sqrt(5f)) / 2.0);
      List<Vector3> vector3List1 = vertices1;
      Vector3 vector3_1 = new Vector3(-1f, num, 0.0f);
      Vector3 vector3_2 = Vector3.op_Multiply(((Vector3) ref vector3_1).normalized, radius);
      vector3List1.Add(vector3_2);
      List<Vector3> vector3List2 = vertices1;
      Vector3 vector3_3 = new Vector3(1f, num, 0.0f);
      Vector3 vector3_4 = Vector3.op_Multiply(((Vector3) ref vector3_3).normalized, radius);
      vector3List2.Add(vector3_4);
      List<Vector3> vector3List3 = vertices1;
      Vector3 vector3_5 = new Vector3(-1f, -num, 0.0f);
      Vector3 vector3_6 = Vector3.op_Multiply(((Vector3) ref vector3_5).normalized, radius);
      vector3List3.Add(vector3_6);
      List<Vector3> vector3List4 = vertices1;
      Vector3 vector3_7 = new Vector3(1f, -num, 0.0f);
      Vector3 vector3_8 = Vector3.op_Multiply(((Vector3) ref vector3_7).normalized, radius);
      vector3List4.Add(vector3_8);
      List<Vector3> vector3List5 = vertices1;
      Vector3 vector3_9 = new Vector3(0.0f, -1f, num);
      Vector3 vector3_10 = Vector3.op_Multiply(((Vector3) ref vector3_9).normalized, radius);
      vector3List5.Add(vector3_10);
      List<Vector3> vector3List6 = vertices1;
      Vector3 vector3_11 = new Vector3(0.0f, 1f, num);
      Vector3 vector3_12 = Vector3.op_Multiply(((Vector3) ref vector3_11).normalized, radius);
      vector3List6.Add(vector3_12);
      List<Vector3> vector3List7 = vertices1;
      Vector3 vector3_13 = new Vector3(0.0f, -1f, -num);
      Vector3 vector3_14 = Vector3.op_Multiply(((Vector3) ref vector3_13).normalized, radius);
      vector3List7.Add(vector3_14);
      List<Vector3> vector3List8 = vertices1;
      Vector3 vector3_15 = new Vector3(0.0f, 1f, -num);
      Vector3 vector3_16 = Vector3.op_Multiply(((Vector3) ref vector3_15).normalized, radius);
      vector3List8.Add(vector3_16);
      List<Vector3> vector3List9 = vertices1;
      Vector3 vector3_17 = new Vector3(num, 0.0f, -1f);
      Vector3 vector3_18 = Vector3.op_Multiply(((Vector3) ref vector3_17).normalized, radius);
      vector3List9.Add(vector3_18);
      List<Vector3> vector3List10 = vertices1;
      Vector3 vector3_19 = new Vector3(num, 0.0f, 1f);
      Vector3 vector3_20 = Vector3.op_Multiply(((Vector3) ref vector3_19).normalized, radius);
      vector3List10.Add(vector3_20);
      List<Vector3> vector3List11 = vertices1;
      Vector3 vector3_21 = new Vector3(-num, 0.0f, -1f);
      Vector3 vector3_22 = Vector3.op_Multiply(((Vector3) ref vector3_21).normalized, radius);
      vector3List11.Add(vector3_22);
      List<Vector3> vector3List12 = vertices1;
      Vector3 vector3_23 = new Vector3(-num, 0.0f, 1f);
      Vector3 vector3_24 = Vector3.op_Multiply(((Vector3) ref vector3_23).normalized, radius);
      vector3List12.Add(vector3_24);
      List<SilentSphere.TriangleIndices> triangleIndicesList1 = new List<SilentSphere.TriangleIndices>()
      {
        new SilentSphere.TriangleIndices(0, 11, 5),
        new SilentSphere.TriangleIndices(0, 5, 1),
        new SilentSphere.TriangleIndices(0, 1, 7),
        new SilentSphere.TriangleIndices(0, 7, 10),
        new SilentSphere.TriangleIndices(0, 10, 11),
        new SilentSphere.TriangleIndices(1, 5, 9),
        new SilentSphere.TriangleIndices(5, 11, 4),
        new SilentSphere.TriangleIndices(11, 10, 2),
        new SilentSphere.TriangleIndices(10, 7, 6),
        new SilentSphere.TriangleIndices(7, 1, 8),
        new SilentSphere.TriangleIndices(3, 9, 4),
        new SilentSphere.TriangleIndices(3, 4, 2),
        new SilentSphere.TriangleIndices(3, 2, 6),
        new SilentSphere.TriangleIndices(3, 6, 8),
        new SilentSphere.TriangleIndices(3, 8, 9),
        new SilentSphere.TriangleIndices(4, 9, 5),
        new SilentSphere.TriangleIndices(2, 4, 11),
        new SilentSphere.TriangleIndices(6, 2, 10),
        new SilentSphere.TriangleIndices(8, 6, 7),
        new SilentSphere.TriangleIndices(9, 8, 1)
      };
      for (int index1 = 0; (double) index1 < (double) recursionLevel; ++index1)
      {
        List<SilentSphere.TriangleIndices> triangleIndicesList2 = new List<SilentSphere.TriangleIndices>();
        for (int index2 = 0; index2 < triangleIndicesList1.Count; ++index2)
        {
          SilentSphere.TriangleIndices triangleIndices = triangleIndicesList1[index2];
          int middlePoint1 = SilentSphere.GetMiddlePoint(triangleIndices.v1, triangleIndices.v2, ref vertices1, ref cache, radius);
          int middlePoint2 = SilentSphere.GetMiddlePoint(triangleIndices.v2, triangleIndices.v3, ref vertices1, ref cache, radius);
          int middlePoint3 = SilentSphere.GetMiddlePoint(triangleIndices.v3, triangleIndices.v1, ref vertices1, ref cache, radius);
          triangleIndicesList2.Add(new SilentSphere.TriangleIndices(triangleIndices.v1, middlePoint1, middlePoint3));
          triangleIndicesList2.Add(new SilentSphere.TriangleIndices(triangleIndices.v2, middlePoint2, middlePoint1));
          triangleIndicesList2.Add(new SilentSphere.TriangleIndices(triangleIndices.v3, middlePoint3, middlePoint2));
          triangleIndicesList2.Add(new SilentSphere.TriangleIndices(middlePoint1, middlePoint2, middlePoint3));
        }
        triangleIndicesList1 = triangleIndicesList2;
      }
      mesh2.vertices = vertices1.ToArray();
      List<int> intList = new List<int>();
      for (int index = 0; index < triangleIndicesList1.Count; ++index)
      {
        intList.Add(triangleIndicesList1[index].v1);
        intList.Add(triangleIndicesList1[index].v2);
        intList.Add(triangleIndicesList1[index].v3);
      }
      mesh2.triangles = intList.ToArray();
      Vector3[] vertices2 = mesh2.vertices;
      Vector2[] vector2Array = new Vector2[vertices2.Length];
      for (int index = 0; index < vertices2.Length; ++index)
      {
        Vector3 normalized = ((Vector3) ref vertices2[index]).normalized;
        Vector2 vector2_1;
        // ISSUE: explicit constructor call
        ((Vector2) ref vector2_1).\u002Ector(0.0f, 0.0f);
        vector2_1.x = (float) (((double) Mathf.Atan2(normalized.x, normalized.z) + 3.1415927410125732) / 3.1415927410125732 / 2.0);
        vector2_1.y = (float) (((double) Mathf.Acos(normalized.y) + 3.1415927410125732) / 3.1415927410125732 - 1.0);
        Vector2 vector2_2 = vector2_1;
        vector2Array[index] = new Vector2(vector2_2.x, vector2_2.y);
      }
      mesh2.uv = vector2Array;
      Vector3[] vector3Array1 = new Vector3[vertices1.Count];
      for (int index3 = 0; index3 < vector3Array1.Length; ++index3)
      {
        Vector3[] vector3Array2 = vector3Array1;
        int index4 = index3;
        Vector3 vector3_25 = vertices1[index3];
        Vector3 normalized = ((Vector3) ref vector3_25).normalized;
        vector3Array2[index4] = normalized;
      }
      mesh2.normals = vector3Array1;
      mesh2.RecalculateBounds();
      gameObject.AddComponent<MeshCollider>().sharedMesh = mesh2;
      return gameObject;
    }

    public struct TriangleIndices
    {
      public int v1;
      public int v2;
      public int v3;

      public TriangleIndices(int v1, int v2, int v3)
      {
        this.v1 = v1;
        this.v2 = v2;
        this.v3 = v3;
      }
    }
  }
}
