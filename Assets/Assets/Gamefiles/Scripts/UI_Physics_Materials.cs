using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UI_Physics_Materials : MonoBehaviour
{
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

    // Start is called before the first frame update
    void Start()
    {
        //set Starting materials values
        //ball
        SetBallMaterialData(rubberBallMaterial.bounciness, rubberBallMaterial.dynamicFriction, rubberBallMaterial.staticFriction);
        //glass
        SetGlassMaterialData(glassBoardMaterial.dynamicFriction, glassBoardMaterial.bounciness, glassBoardMaterial.staticFriction);
       
        //iron
        SetIronHoopMaterialData(ironHoopMaterial.bounciness, ironHoopMaterial.dynamicFriction, ironHoopMaterial.staticFriction);

        //court
        SetCourtMaterialsData(woodCourtMaterial.bounciness, woodCourtMaterial.dynamicFriction, woodCourtMaterial.staticFriction);
     

        //Event Listeners
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
    }

    //ball material  event  methods
    public void OnBallDynamicFrictionChanged(float rate) { rubberBallMaterial.dynamicFriction = rate; tBallDynamicFrictionText.text = rate.ToString("f2"); }
    public void OnBallStaticFrictionChanged(float rate) { rubberBallMaterial.staticFriction = rate; tBallStaticFrictrionText.text = rate.ToString("f2"); }
    public void OnBallBounchChanged(float rate) { rubberBallMaterial.bounciness = rate; tBallBounchnessText.text = rate.ToString("f2"); }

    // glass materials event methods
    public void OnGlassDynamicFrictionChanged(float rate) { glassBoardMaterial.dynamicFriction = rate; tGlasBoardDynamicFrictionText.text = rate.ToString("f2"); }
    public void OnGlassStaticFrictionChanged(float rate) { glassBoardMaterial.staticFriction = rate; tGlassBoardStaticFrictrionText.text = rate.ToString("f2"); }
    public void OnGlassBounchChanged(float rate) { glassBoardMaterial.bounciness = rate; tGlassBoardBounchnessText.text = rate.ToString("f2"); }

    //court materiaL event methods

    public void OnCourtDynamicFrictionChanged(float rate) { woodCourtMaterial.dynamicFriction = rate; tCourtDynamicFrictionText.text = rate.ToString("f2"); }
    public void OnCourtStaticFrictionChanged(float rate) { woodCourtMaterial.staticFriction = rate; tCourtStaticFrictrionText.text = rate.ToString("f2"); }
    public void OnCourtBounchChanged(float rate) { woodCourtMaterial.bounciness = rate; tCourtBounchnessText.text = rate.ToString("f2"); }

    //iron material event methods
    public void OnIronHoopDynamicFrictionChanged(float rate) { ironHoopMaterial.dynamicFriction = rate; tIronDynamicFrictionText.text = rate.ToString("f2"); }
    public void OnIronHoopStaticFrictionChanged(float rate) { ironHoopMaterial.staticFriction = rate; tIronStaticFrictrionText.text = rate.ToString("f2"); }
    public void OnIronHoopBounchChanged(float rate) { ironHoopMaterial.bounciness = rate; tIronBounchnessText.text = rate.ToString("f2"); }


    // initial set up off UI values based on stored data materal
    private void SetBallMaterialData(float bounciness, float dynamicFriction, float staticFriction)
    {
        sBallBounchness.value = bounciness; sBallDynamicFriction.value = dynamicFriction; sBallStaticFrictrion.value = staticFriction;
        tBallDynamicFrictionText.text = dynamicFriction.ToString("f2"); tBallStaticFrictrionText.text = staticFriction.ToString("f2"); tBallBounchnessText.text = bounciness.ToString("f2");
    }

    private void SetIronHoopMaterialData(float bounciness, float dynamicFriction, float staticFriction)
    {
        sIronBounchness.value = bounciness; sIronDynamicFriction.value = dynamicFriction; sIronStaticFrictrion.value = staticFriction;
        tIronDynamicFrictionText.text = dynamicFriction.ToString("f2"); tIronStaticFrictrionText.text = staticFriction.ToString("f2"); tIronBounchnessText.text = bounciness.ToString("f2");
    }

    private void SetGlassMaterialData(float dynamicFriction, float bounciness, float staticFriction)
    {
        sGlassBounchness.value = bounciness; sGlassDynamicFriction.value = dynamicFriction; sGlassStaticFrictrion.value = staticFriction;
        tGlasBoardDynamicFrictionText.text = dynamicFriction.ToString("f2"); tGlassBoardStaticFrictrionText.text = staticFriction.ToString("f2"); tGlassBoardBounchnessText.text = bounciness.ToString("f2");
    }

    private void SetCourtMaterialsData(float bounciness, float dynamicFriction, float staticFriction)
    {
        sCourtBounchness.value = bounciness; sCourtDynamicFriction.value = dynamicFriction; sCourtStaticFrictrion.value = staticFriction;
        tCourtDynamicFrictionText.text = dynamicFriction.ToString("f2"); tCourtStaticFrictrionText.text = staticFriction.ToString("f2"); tCourtBounchnessText.text = bounciness.ToString("f2");
    }
}
