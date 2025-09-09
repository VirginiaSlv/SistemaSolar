// 22130
// 22153

using UnityEngine;
using System.Collections;
using UnityEditor;

public class PlanetClick : MonoBehaviour
{
    public GameObject target;      // alvo que a câmera deve focar
    public AudioSource planetMusic; // música do planeta
    public ChangeLookAtTarget cameraController;

    void Start()
	{
        // assume que o próprio objeto (gameObject) é o alvo.
        if (target == null)
            target = gameObject;

        // o script tenta encontrar e atribuir o componente 'ChangeLookAtTarget' da câmera principal (Main Camera) a ela.
        if (cameraController == null)
            cameraController = Camera.main.GetComponent<ChangeLookAtTarget>();
    }

    // O método 'OnMouseDown' é executado quando você clica em um planeta
    void OnMouseDown()
    {
        // troca o alvo da instância da câmera
        cameraController.target = target;

        if (planetMusic != null)
        {
            MusicManager.Instance.PlayMusic(planetMusic, 2f);
        }

        // calcula a nova posição da câmera para visualizar o planeta
        Vector3 direction = (Camera.main.transform.position - target.transform.position).normalized;
        float distance = target.transform.localScale.x * 2.5f;

        // move a câmera para a nova posição e olha para o alvo
        Camera.main.transform.position = target.transform.position + direction * distance;
        Camera.main.transform.LookAt(target.transform);

        // define o campo de visão para um valor fixo, evitando a distorção.
        Camera.main.fieldOfView = 60;
    }
}
