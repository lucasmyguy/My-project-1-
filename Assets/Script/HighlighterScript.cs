using System.Collections.Generic;
using UnityEngine;

public class HoverHighlighter : MonoBehaviour
{
    public Camera cam;
    public LayerMask highlightLayers;
    public Color highlightColor = Color.yellow;
    public bool showDebugLabel = true;

    private Transform hoveredRoot;
    private readonly List<Renderer> renderers = new List<Renderer>();
    private readonly List<Color> originalColors = new List<Color>();

    private RaycastHit lastHit;
    private bool hasHit;

    void Awake()
    {
        if (cam == null) cam = Camera.main;
    }

    void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);


        hasHit = Physics.Raycast(ray, out lastHit, Mathf.Infinity, ~0, QueryTriggerInteraction.Ignore);

        Transform newRoot = null;

        if (hasHit && IsInMask(lastHit.collider.gameObject.layer))
        {
            StatsComponent s = lastHit.collider.GetComponentInParent<StatsComponent>();
            newRoot = s != null ? s.transform : lastHit.collider.transform;
        }

        if (newRoot != hoveredRoot)
        {
            ClearHighlight();
            if (newRoot != null) ApplyHighlight(newRoot);
        }
    }

    bool IsInMask(int layer)
    {
        return (highlightLayers.value & (1 << layer)) != 0;
    }

    void ApplyHighlight(Transform root)
    {
        hoveredRoot = root;

        foreach (Renderer r in root.GetComponentsInChildren<Renderer>())
        {
            renderers.Add(r);
            originalColors.Add(r.material.color);
            r.material.color = highlightColor;
        }
    }

    void ClearHighlight()
    {
        for (int i = 0; i < renderers.Count; i++)
        {
            if (renderers[i] != null)
            {
                renderers[i].material.color = originalColors[i];
            }
        }

        renderers.Clear();
        originalColors.Clear();
        hoveredRoot = null;
    }

    //void OnGUI()
    //{
    //    if (!showDebugLabel || !hasHit || lastHit.collider == null) return;

    //    GameObject go = lastHit.collider.gameObject;
    //    string text = $"{go.name}\nlayer {go.layer} ({LayerMask.LayerToName(go.layer)})\ndist {lastHit.distance:F1}";

    //    Vector2 pos = Input.mousePosition;
    //    GUI.Box(new Rect(pos.x + 15, Screen.height - pos.y + 15, 190, 55), text);
    //}

    void OnDrawGizmos()
    {
        if (!Application.isPlaying || !hasHit || lastHit.collider == null) return;

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(lastHit.point, 0.2f);

        Bounds b = lastHit.collider.bounds;
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(b.center, b.size);
    }
}