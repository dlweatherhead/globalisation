using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(AudioSource))]
public class Nation : MonoBehaviour {
    [SerializeField] [Range(0, 100)] public int economicLevel = 1;
    [SerializeField] [Range(0, 100)] public int environmentalLevel = 1;
    [SerializeField] [Range(0, 100)] public int socialLevel = 1;

    [SerializeField] [Range(-10, 10)] public int socialEconomicInfluencerScale = 1;
    [SerializeField] [Range(-10, 10)] public int economicEnvironmentalInfluencerScale = 1;
    [SerializeField] [Range(-10, 10)] public int environmentalSocialInfluencerScale = 1;

    private Color color;
    private MeshRenderer[] meshRenderers;

    private AudioSource audioSource;

    bool isSelected;

    void Awake() {
        meshRenderers = GetComponentsInChildren<MeshRenderer>();
        audioSource = GetComponent<AudioSource>();

        foreach (MeshRenderer meshRenderer in meshRenderers) {
            color = meshRenderer.material.color;
        }
    }

    public void UpdateSocial(int deltaSocial) {
        socialLevel += deltaSocial;
        economicLevel -= deltaSocial * socialEconomicInfluencerScale / 2;

        GameStateManager.instance.UpdateSocialValue(socialLevel);
        GameStateManager.instance.UpdateEconomicValue(economicLevel);
    }

    public void UpdateEconomic(int deltaEconomic) {
        economicLevel += deltaEconomic;
        environmentalLevel -= deltaEconomic * economicEnvironmentalInfluencerScale / 2;

        GameStateManager.instance.UpdateEconomicValue(economicLevel);
        GameStateManager.instance.UpdateEnvironmentalValue(environmentalLevel);
    }

    public void UpdateEnvironmental(int deltaEnvironmental) {
        environmentalLevel += deltaEnvironmental;
        socialLevel -= deltaEnvironmental * environmentalSocialInfluencerScale / 2;

        GameStateManager.instance.UpdateEnvironmentalValue(environmentalLevel);
        GameStateManager.instance.UpdateSocialValue(socialLevel);
    }

    public Color GetColorWhenNationSelected() {
        return color;
    }

    void OnMouseDown() {
        audioSource.Play();
        GameStateManager.instance.NationClickListener(this);
    }

    public void Select() {
        foreach (MeshRenderer meshRenderer in meshRenderers) {
            meshRenderer.material.shader = Shader.Find("Self-Illumin/Outlined Diffuse");
        }
    }

    public void Deselect() {
        foreach (MeshRenderer meshRenderer in meshRenderers) {
            meshRenderer.material.shader = Shader.Find("Diffuse");
        }
    }
}