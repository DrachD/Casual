using System.Collections;
using System.Collections.Generic;
using UnityEngine;

////////////////////////////////////////////////////////
// raycast visibility system
////////////////////////////////////////////////////////
public class FieldOfView : MonoBehaviour
{
    [SerializeField] float _viewRadius;

    public float ViewRadius => _viewRadius;

    [Range(0, 360)]
    [SerializeField] float _viewAngle;

    public float ViewAngle => _viewAngle;

    public LayerMask targetMask;

    public LayerMask obstacleMask;

    // all items that touch subjects
    public List<Transform> visibleTargets = new List<Transform>();

    public float meshResolution;

    public MeshFilter viewMeshFilter;

    private Mesh _viewMesh;

    private GameMenuController _gameMenuController;

    private MenuPaused _menuPaused;

    private void Awake()
    {
        _viewMesh = new Mesh();
        _viewMesh.name = "View Mesh";
        viewMeshFilter.mesh = _viewMesh;
        _gameMenuController = GameObject.Find("GameMenu Controller").GetComponent<GameMenuController>();
        _menuPaused = GameObject.Find("MenuManager").GetComponent<MenuPaused>();
    }

    private void Start()
    {
        StartCoroutine(FindTargetWithDelay(.1f));
    }

    IEnumerator FindTargetWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    private void LateUpdate()
    {
        DrawFieldOfView();
    }

    private void Update()
    {
        CheckTargets();
    }

    // check if there is an enemy in the radius, if there is, then activate the end of the game
    private void CheckTargets()
    {
        foreach (Transform tran in visibleTargets)
        {
            if (tran.CompareTag("Player"))
            {
                if (_gameMenuController != null)
                {
                    MenuState menuState = _gameMenuController.GetMenuState(State.GameOver);
                    menuState.gameObject.SetActive(true);
                }

                if (_menuPaused != null)
                {
                    _menuPaused.gameOver = true;
                    _menuPaused.isMenuPaused = true;
                }
            }
        }
    }

    // insert all objects in our radius into the list
    private void FindVisibleTargets()
    {
        visibleTargets.Clear();
        Collider[] targetInViewRadius = Physics.OverlapSphere(transform.position, _viewRadius, targetMask);

        for (int i = 0; i < targetInViewRadius.Length; i++)
        {
            Transform target = targetInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < _viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    visibleTargets.Add(target);
                }
            }
        }
    }

    // draw the field of view
    void DrawFieldOfView()
    {
        int stepCount = Mathf.RoundToInt(_viewAngle * meshResolution);
        float stepAngleSize = _viewAngle / stepCount;
        List<Vector3> viewPoints = new List<Vector3>();

        for (int i = 0; i <= stepCount; i++)
        {
            float angle = transform.eulerAngles.y - _viewAngle / 2 + stepAngleSize * i;
            ViewCastInfo newViewCast = ViewCast(angle);
            viewPoints.Add(newViewCast.point);
        }

        int vertexCount = viewPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - 1) * 3];

        vertices[0] = Vector3.zero;
        for (int i = 0; i < vertexCount - 1; i++)
        {
            vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);

            if (i < vertexCount - 2)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
        }

        _viewMesh.Clear();
        _viewMesh.vertices = vertices;
        _viewMesh.triangles = triangles;
        _viewMesh.RecalculateNormals();
    }

    ViewCastInfo ViewCast(float globalAngle)
    {
        Vector3 dir = DirFromAngle(globalAngle, true);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, dir, out hit, _viewRadius, obstacleMask))
        {
            return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
        }
        else
        {
            return new ViewCastInfo(false, transform.position + dir * _viewRadius, _viewRadius, globalAngle);
        }
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    public struct ViewCastInfo
    {
        public bool hit;
        public Vector3 point;
        public float dst;
        public float angle;

        public ViewCastInfo(bool _hit, Vector3 _point, float _dst, float _angle)
        {
            hit = _hit;
            point = _point;
            dst = _dst;
            angle = _angle;
        }
    }
}
