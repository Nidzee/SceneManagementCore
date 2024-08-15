using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using DG.Tweening;




public class VisualsHandler : MonoBehaviour
{
    [Header("Hit materials collection")]
    [SerializeField] protected List<Renderer> _meshRenderers = new List<Renderer>();


    [Header("Particles config")]
    [SerializeField] protected ParticleSystem _deathParticleSystem;


    [Header("Model scale animation config")]
    [SerializeField] Transform _modelTrans;


    [SerializeField] Transform _buffsParticlesContainer;
    public Transform ParticlesContainer => _buffsParticlesContainer;



    const int DELAY_HIT_MATERIAL_MS = 50;
    protected Color _originalMaterialColor;
    protected Color _emissionColor_notHit = Color.black;
    protected Color _emissionColor_hit = Color.white;
    CancellationTokenSource _source;
    List<MeshRendererMaterialCollection> renderesData = new List<MeshRendererMaterialCollection>();





    

    public void Initialize()
    {
        _source = new CancellationTokenSource();

        foreach (var meshRenderer in _meshRenderers)
        {
            MeshRendererMaterialCollection result = new MeshRendererMaterialCollection();
            result.Renderer = meshRenderer;
            result.Materials = new List<Material>();

            foreach (var meshMateial in meshRenderer.materials)
            {
                result.Materials.Add(meshMateial);
            }
            renderesData.Add(result);
        }


        _originalMaterialColor = Color.white;
    }
    public void KillAnimation()
    {
        _source.Cancel();
    }






    // Emission animation
    public void LaunchEnemyModelHitAnimation()
    {
        UpdateEmissionCOlor(_emissionColor_hit);
        DelayMaterialBack(_source.Token).Forget();

        if (_modelTrans != null)
        {
            Vector3 originalScale = _modelTrans.localScale;
            Vector3 shrinkScale = originalScale * 1.1f;

            var sequence = DOTween.Sequence();
            sequence.Append(_modelTrans.DOScale(shrinkScale, 0.15f));
            sequence.Append(_modelTrans.DOScale(originalScale, 0.15f));
        }
    }

    protected void UpdateEmissionCOlor(Color targetColor)
    {
        foreach (var rendererData in renderesData)
            foreach (var mat in rendererData.Materials)
            {
                mat.SetColor("_EmissionColor", targetColor);
                mat.SetColor("_Emission", targetColor);
            }
    }

    async UniTask DelayMaterialBack(CancellationToken token)
    {
        await UniTask.Delay(DELAY_HIT_MATERIAL_MS);
        
        if (token.IsCancellationRequested || gameObject == null)
            return;

        UpdateEmissionCOlor(_emissionColor_notHit);
    }






    // Color animation
    public void ChangeModelColor(Color targetColor)
    {
        UpdateMaterialsColor(targetColor);
    }
    public void ReturnModelOriginalColor()
    {
        UpdateMaterialsColor(_originalMaterialColor);
    }
    void UpdateMaterialsColor(Color targetColor)
    {
        foreach (var rendererData in renderesData)
            foreach (var mat in rendererData.Materials)
                mat.color = targetColor;
    }






    // Particles animations
    public void PlayDeathParticles()
    {
        TryToCreateParticles_Internal(_deathParticleSystem, true);
    }

    public ParticleSystem TryToCreateParticles(ParticleSystem ps, bool release, float scaleMultiplyer)
    {
        return TryToCreateParticles_Internal(ps, release, scaleMultiplyer);
    }

    

    public void TryToPlayParticles(ParticleSystem ps)
    {
        if (ps == null)
            return;

        ps.Play();
    }

    ParticleSystem TryToCreateParticles_Internal(ParticleSystem ps, bool release = false, float scaleMultiplyer = 1)
    {
        if (ps == null)
            return null;


        var particles = Instantiate(ps, ParticlesContainer);
        particles.transform.localScale = Vector3.one * scaleMultiplyer;


        if (release)
            particles.transform.SetParent(null);


        return particles;
    }
}



public class MeshRendererMaterialCollection
{
    public Renderer Renderer;
    public List<Material> Materials;
}