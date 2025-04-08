using System.Collections;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] float interval = 1.5f;
    [SerializeField] GameObject lore1Tutorial;
    [SerializeField] GameObject lore2Tutorial;
    [SerializeField] GameObject lore3Tutorial;

    [SerializeField] GameObject dashTutorial;
    [SerializeField] GameObject spearPokeTutorial;
    [SerializeField] GameObject spearThrowTutorial;
    [SerializeField] GameObject pickupSpearTutorial;
    [SerializeField] GameObject radarOpenTutorial;
    [SerializeField] GameObject radarCloseTutorial;
    [SerializeField] GameObject checkRadarReminderTutorial;
    [SerializeField] GameObject enterSubTutorial;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(interval / 2f);

        if (!GameStateManager.Instance.HasCompletedTutorial1)
        {
            lore1Tutorial.SetActive(true);
            yield return new WaitUntil(() => InputManager.Controls.Actions.Interact.WasPressedThisFrame() || InputManager.Controls.Actions.Dash.WasPressedThisFrame());
            lore1Tutorial.SetActive(false);

            yield return new WaitForSeconds(interval / 2f);

            lore2Tutorial.SetActive(true);
            yield return new WaitUntil(() => InputManager.Controls.Actions.Interact.WasPressedThisFrame());
            lore2Tutorial.SetActive(false);

            yield return new WaitForSeconds(interval / 2f);

            lore3Tutorial.SetActive(true);
            yield return new WaitUntil(() => InputManager.Controls.Actions.Interact.WasPressedThisFrame());
            lore3Tutorial.SetActive(false);

            yield return new WaitForSeconds(interval);

            dashTutorial.SetActive(true);
            yield return new WaitUntil(() => Player.Instance.Movement.IsDashOnCooldown);
            dashTutorial.SetActive(false);

            yield return new WaitForSeconds(interval);

            spearPokeTutorial.SetActive(true);
            yield return new WaitUntil(() => Player.Instance.SpearHandling.IsPoking);
            spearPokeTutorial.SetActive(false);

            yield return new WaitForSeconds(interval);

            spearThrowTutorial.SetActive(true);
            yield return new WaitUntil(() => !Player.Instance.SpearHandling.HasSpear);
            spearThrowTutorial.SetActive(false);

            yield return new WaitForSeconds(interval);

            pickupSpearTutorial.SetActive(true);
            yield return new WaitUntil(() => Player.Instance.SpearHandling.HasSpear);
            pickupSpearTutorial.SetActive(false);

            yield return new WaitForSeconds(interval);

            radarOpenTutorial.SetActive(true);
            yield return new WaitUntil(() => Player.Instance.Radar.IsOpen);
            radarOpenTutorial.SetActive(false);

            radarCloseTutorial.SetActive(true);
            yield return new WaitUntil(() => !Player.Instance.Radar.IsOpen);
            radarCloseTutorial.SetActive(false);
            GameStateManager.Instance.HasCompletedTutorial1 = true;

        }

        if (!GameStateManager.Instance.HasCompletedTutorial2)
        {
            yield return new WaitUntil(() => GameStateManager.Instance.LeviathanIsActive);
        
            checkRadarReminderTutorial.SetActive(true);
            yield return new WaitUntil(() => Player.Instance.Radar.IsOpen);
            checkRadarReminderTutorial.SetActive(false);
        
            GameStateManager.Instance.HasCompletedTutorial2 = true;
        }

        yield return new WaitUntil(() => GameStateManager.Instance.CollectedParts.Count >= 2);
        enterSubTutorial.SetActive(true);

        yield return new WaitUntil(() => !Player.Instance.gameObject.activeInHierarchy);
        enterSubTutorial.SetActive(false);
        
        GameStateManager.Instance.HasCompletedTutorial3 = true;
    }

}
