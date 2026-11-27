using System.Collections.Generic;
using UnityEngine;

public class CopyHandPoseByName : MonoBehaviour
{
    public Transform sourceRoot;
    public Transform targetRoot;

    [Header("Wrist Alignment")]
    public Vector3 wristRotationOffset = new Vector3(0f, 180f, 0f);

    [Header("Flip Rotation Around Local Axis")]
    public bool flipX = false;
    public bool flipY = false;
    public bool flipZ = false;

    private class BonePair
    {
        public Transform source;
        public Transform target;
        public Quaternion sourceStartLocal;
        public Quaternion targetStartLocal;
        public bool isWrist;
    }

    private readonly List<BonePair> _pairs = new();

    private void Start()
    {
        if (sourceRoot == null) sourceRoot = transform;
        if (targetRoot == null) targetRoot = transform;

        var sourceByName = new Dictionary<string, Transform>();
        foreach (var t in sourceRoot.GetComponentsInChildren<Transform>(true))
        {
            sourceByName.TryAdd(t.name, t);
        }

        foreach (var targetBone in targetRoot.GetComponentsInChildren<Transform>(true))
        {
            if (!sourceByName.TryGetValue(targetBone.name, out var sourceBone)) continue;

            _pairs.Add(new BonePair
            {
                source = sourceBone,
                target = targetBone,
                sourceStartLocal = sourceBone.localRotation,
                targetStartLocal = targetBone.localRotation,
                isWrist = targetBone.name.Contains("Wrist")
            });
        }
    }

    private static Quaternion FlipAxis(Quaternion q, bool fx, bool fy, bool fz)
    {
        float x = fx ? -q.x : q.x;
        float y = fy ? -q.y : q.y;
        float z = fz ? -q.z : q.z;
        return new Quaternion(x, y, z, q.w).normalized;
    }

    private void LateUpdate()
    {
        if (targetRoot != null && sourceRoot != null)
        {
            targetRoot.position = sourceRoot.position;
            targetRoot.rotation = sourceRoot.rotation * Quaternion.Euler(wristRotationOffset);
        }

        foreach (var p in _pairs)
        {
            if (p.source == null || p.target == null || p.isWrist) continue;

            Quaternion delta = Quaternion.Inverse(p.sourceStartLocal) * p.source.localRotation;
            Quaternion adjustedDelta = FlipAxis(delta, flipX, flipY, flipZ);

            p.target.localRotation = p.targetStartLocal * adjustedDelta;
        }
    }
}