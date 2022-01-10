using UnityEngine;

public class Scope : MonoBehaviour
{
    [SerializeField] private ScopeData scopeData;
    private LensTransforms lensTransforms;
    [SerializeField] private ScopeCulling scopeCulling;

    [SerializeField] private float _strength;
    public float strength { 
        get => _strength; 
        private set => _strength = value;
    }

    private Material opticMat => scopeData.renderer.material;


    private void Start()
    {
        scopeData.renderer = scopeData.lensTransform.GetComponent<MeshRenderer>();
        scopeData.Setup();
        lensTransforms = new LensTransforms(scopeData);
        scopeCulling.Setup(scopeData);

        UpdateStrength();
    }

    private void Update()
    {
        if( scopeCulling.CullingTest() == false ) { return; }

        lensTransforms.BindLensCamera();
    }

    public void SetStrength(float strength)
    {
        this.strength = strength;
        UpdateStrength();
    }

    private void UpdateStrength()
    {
        opticMat.SetFloat("_Strength", strength);
    }
}
