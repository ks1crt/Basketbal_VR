using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Assets.HurricaneVR.Framework.Shared.Utilities;
using HurricaneVR.Framework.Components;
using HurricaneVR.Framework.ControllerInput;
using HexabodyVR.PlayerController;
using UnityEngine.UI;
using TMPro;
using HurricaneVR.Framework.Core.Bags;
using HurricaneVR.Framework.Core.HandPoser;
using HurricaneVR.Framework.Core.HandPoser.Data;
using HurricaneVR.Framework.Core.Player;
using HurricaneVR.Framework.Core.ScriptableObjects;
using HurricaneVR.Framework.Core.Utils;
using HurricaneVR.Framework.Shared;
using HurricaneVR.Framework.Shared.Utilities;
using UnityEngine.Events;

namespace HurricaneVR.Framework.Core
{
    public class Bounch_Ball : MonoBehaviour
    {


        // colliders
        public Rigidbody Basketball;
        public SphereCollider ballCollider;
        //public BoxCollider glassCollier;
        //public BoxCollider courtCollider;
        //public MeshCollider hoopCollider;
        //physics material
        public PhysicMaterial rubberBallMaterial;
        public PhysicMaterial glassBoardMaterial;
        public PhysicMaterial woodCourtMaterial;
        public PhysicMaterial ironHoopMaterial;

        // ball sliders
        public Slider sBallDynamicFriction;
        public TextMeshProUGUI tBallDynamicFrictionText;
        public Slider sBallStaticFrictrion;
        public TextMeshProUGUI tBallStaticFrictrionText;
        public Slider sBallBounchness;
        public TextMeshProUGUI tBallBounchnessText;
        //Glass sliders
        public Slider sGlassDynamicFriction;
        public TextMeshProUGUI tGlasBoardDynamicFrictionText;
        public Slider sGlassStaticFrictrion;
        public TextMeshProUGUI tGlassBoardStaticFrictrionText;
        public Slider sGlassBounchness;
        public TextMeshProUGUI tGlassBoardBounchnessText;
        //court sliders
        public Slider sCourtDynamicFriction;
        public TextMeshProUGUI tCourtDynamicFrictionText;
        public Slider sCourtStaticFrictrion;
        public TextMeshProUGUI tCourtStaticFrictrionText;
        public Slider sCourtBounchness;
        public TextMeshProUGUI tCourtBounchnessText;
        //iron sliders
        public Slider sIronDynamicFriction;
        public TextMeshProUGUI tIronDynamicFrictionText;
        public Slider sIronStaticFrictrion;
        public TextMeshProUGUI tIronStaticFrictrionText;
        public Slider sIronBounchness;
        public TextMeshProUGUI tIronBounchnessText;
        //
        public Button saveButton;

        public void Awake()
        {
            
           

        }
        // Start is called before the first frame update
        void Start()
        {
            Basketball.mass = 0.624f;
            //ball listeners
            sBallDynamicFriction.onValueChanged.AddListener(OnBallDynamicFrictionChanged);
            sBallStaticFrictrion.onValueChanged.AddListener(OnBallStaticFrictionChanged);
            sBallBounchness.onValueChanged.AddListener(OnBallBounchChanged);
            //Glass Listeners
            sGlassDynamicFriction.onValueChanged.AddListener(OnGlassDynamicFrictionChanged);
            sGlassStaticFrictrion.onValueChanged.AddListener(OnGlassStaticFrictionChanged);
            sGlassBounchness.onValueChanged.AddListener(OnGlassBounchChanged);
            //Court Listeners
            sCourtDynamicFriction.onValueChanged.AddListener(OnCourtDynamicFrictionChanged);
            sCourtStaticFrictrion.onValueChanged.AddListener(OnCourtStaticFrictionChanged);
            sCourtBounchness.onValueChanged.AddListener(OnCourtBounchChanged);
            // Iron Hoop Listeners
            sIronDynamicFriction.onValueChanged.AddListener(OnIronHoopDynamicFrictionChanged);
            sIronStaticFrictrion.onValueChanged.AddListener(OnIronHoopStaticFrictionChanged);
            sIronBounchness.onValueChanged.AddListener(OnIronHoopBounchChanged);
            // save
            //saveButton.onClick.AddListener(SavePhysicsData);
        }

       

        //ball material  methods
        public void OnBallDynamicFrictionChanged(float rate) {rubberBallMaterial.dynamicFriction = rate; tBallDynamicFrictionText.text = rate.ToString("f2");}
        public void OnBallStaticFrictionChanged(float rate) { rubberBallMaterial.staticFriction = rate; tBallStaticFrictrionText.text = rate.ToString("f2"); }
        public void OnBallBounchChanged(float rate) { rubberBallMaterial.bounciness = rate; tBallBounchnessText.text = rate.ToString("f2");}

        // glass materials methods
        public void OnGlassDynamicFrictionChanged(float rate) { glassBoardMaterial.dynamicFriction = rate; tGlasBoardDynamicFrictionText.text = rate.ToString("f2"); }
        public void OnGlassStaticFrictionChanged(float rate) { glassBoardMaterial.staticFriction = rate; tGlassBoardStaticFrictrionText.text = rate.ToString("f2"); }
        public void OnGlassBounchChanged(float rate) { glassBoardMaterial.bounciness = rate; tGlassBoardBounchnessText.text = rate.ToString("f2"); }

        //court materiaL methods

        public void OnCourtDynamicFrictionChanged(float rate) { woodCourtMaterial.dynamicFriction = rate; tCourtDynamicFrictionText.text = rate.ToString("f2"); }
        public void OnCourtStaticFrictionChanged(float rate) { woodCourtMaterial.staticFriction = rate; tCourtStaticFrictrionText.text = rate.ToString("f2"); }
        public void OnCourtBounchChanged(float rate) { woodCourtMaterial.bounciness = rate; tCourtBounchnessText.text = rate.ToString("f2"); }

        //iron material methods
        public void OnIronHoopDynamicFrictionChanged(float rate) { ironHoopMaterial.dynamicFriction = rate; tIronDynamicFrictionText.text = rate.ToString("f2"); }
        public void OnIronHoopStaticFrictionChanged(float rate) { ironHoopMaterial.staticFriction = rate; tIronStaticFrictrionText.text = rate.ToString("f2"); }
        public void OnIronHoopBounchChanged(float rate) { ironHoopMaterial.bounciness = rate; tIronBounchnessText.text = rate.ToString("f2"); }

        //public void SavePhysicsData()
        //{
        //    PlayerPrefs.SetFloat("ball dynamic friction", rubberBallMaterial.dynamicFriction);
        //    PlayerPrefs.SetFloat("ball static friction", rubberBallMaterial.staticFriction);
        //    PlayerPrefs.SetFloat("ball bouynches", rubberBallMaterial.bounciness);
        //    PlayerPrefs.SetFloat("glass dynamic friction", glassBoardMaterial.dynamicFriction);
        //    PlayerPrefs.SetFloat("glass static friction", glassBoardMaterial.staticFriction);
        //    PlayerPrefs.SetFloat("glass bouynches", glassBoardMaterial.bounciness);
        //    PlayerPrefs.SetFloat("wood dynamic friction", woodCourtMaterial.dynamicFriction);
        //    PlayerPrefs.SetFloat("wood static friction", woodCourtMaterial.staticFriction);
        //    PlayerPrefs.SetFloat("wood bouynches", woodCourtMaterial.bounciness);
        //    PlayerPrefs.SetFloat("iron dynamic friction", ironHoopMaterial.dynamicFriction);
        //    PlayerPrefs.SetFloat("iron static friction", ironHoopMaterial.staticFriction);
        //    PlayerPrefs.SetFloat("iron bouynches", ironHoopMaterial.bounciness);
        //    PlayerPrefs.Save();
        //}
        

    }
}