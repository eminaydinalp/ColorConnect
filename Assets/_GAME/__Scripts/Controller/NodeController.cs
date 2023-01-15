using System.Collections;
using System.Collections.Generic;
using _GAME.__Scripts._Managers;
using _GAME.__Scripts.Liquid;
using Lean.Touch;
using UnityEngine;

namespace _GAME.__Scripts.Controller
{
    public class NodeController : RMonoBehaviour
    {
        public List<CubeController> cubeControllers;
        
        public Transform clampYMinObject;
        public Transform clampYMaxObject;

        public DirectionFlow directionFlow;

        public Pipe pipe;
        public MeshRenderer obiSolverMesh;

        private NodeController _inputNode;

        public bool isTriggerSelf;
        public bool isTriggerTarget;

        Camera camera;

        private Vector3 _firstPoint;

        [SerializeField] private float radius;

        public bool isDrag;
        
        public bool isObiClosed;

        public bool isUp;


        private void Awake()
        {
            camera = Camera.main;
        }

        private void Start()
        {
            obiSolverMesh.enabled = false;
            _firstPoint = transform.position;

            StartInvokeFindInput();
        }

        private void OnEnable()
        {
            LeanTouch.OnFingerUp += finger => isTriggerSelf = false;
            LeanTouch.OnFingerUpdate += Drag;
        }

        private void OnDisable()
        {
            LeanTouch.OnFingerUp -= finger => isTriggerSelf = false;
            LeanTouch.OnFingerUpdate -= Drag;
        }


        private void Update()
        {
            if (!isTriggerSelf && !isTriggerTarget)
            {
                ComeBack();
            }
        }
        
        private void Drag(LeanFinger obj)
        {
            if (isTriggerSelf)
            {
                FollowMousePosition();
                obiSolverMesh.enabled = true;

                isDrag = true;
                isObiClosed = false;
            }
        }

        private void FindInput()
        {
            if (!isDrag) return;

            Collider[] hitColliders =
                Physics.OverlapSphere(transform.position, radius, LayerMask.GetMask("InputTarget"));

            if (hitColliders.Length > 1)
            {
                Debug.Log(hitColliders.Length);
                
                for (int i = 0; i < hitColliders.Length; i++)
                {
                    if (!hitColliders[i].GetComponent<NodeController>().isDrag)
                    {
                        _inputNode = hitColliders[i].GetComponent<NodeController>();
                    }
                }
                
                if(cubeControllers[0].rowCount == _inputNode.cubeControllers[0].rowCount || _inputNode.isUp == isUp) return;
                
                isTriggerTarget = true;
                isTriggerSelf = false;

                CancelInvoke(nameof(FindInput));
                
                transform.position = _inputNode.transform.position;


                ChargeManager.Instance.outputNode = this;
                ChargeManager.Instance.inputNode = _inputNode;
                ChargeManager.Instance.inputDirectionFlow = _inputNode.directionFlow;
                ChargeManager.Instance.outputDirectionFlow = directionFlow;
                ChargeManager.Instance.pipe = pipe;

                ChargeManager.Instance.ChargeProcess();
            }
        }

        public void StartInvokeFindInput()
        {
            InvokeRepeating(nameof(FindInput), 1, 0.2f);
        }

        private void ComeBack()
        {
            transform.position = Vector3.Lerp(transform.position, _firstPoint, Time.deltaTime * 10);
            isDrag = false;

            StartCoroutine(CloseObiSolver());
        }

        private void FollowMousePosition()
        {
            Vector3 cameraPos = camera.ScreenToWorldPoint(Input.mousePosition);
            
            Vector3 target = new Vector3(cameraPos.x, cameraPos.y + 0.3f, transform.position.z);
            transform.position = target;

            ClampYPosition();
        }

        private void ClampYPosition()
        {
            var yPos = transform.position.y;
            yPos = Mathf.Clamp(yPos, clampYMinObject.transform.localPosition.y,
                clampYMaxObject.transform.localPosition.y);
            transform.position = new Vector3(transform.position.x, yPos, transform.position.z);
        }

        private IEnumerator CloseObiSolver()
        {
            if (isObiClosed) yield break;

            isObiClosed = true;
            yield return new WaitForSeconds(0.4f);

            if (isDrag) yield break;

            obiSolverMesh.enabled = false;
        }
    }
}