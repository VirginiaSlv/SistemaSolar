// 22130
// 22153

using UnityEngine;
using System.Collections;

public class ChangeLookAtTarget : MonoBehaviour {

    public GameObject target;    // é o GameObject que a câmera deve focar.
    public AudioSource music;    // é a fonte de áudio associada a este objeto, usada para reproduzir a música quando o objeto for clicado.

    public float cameraDistance = 2f;   // define a distância que a câmera ficará do alvo.

    void Start()
    {
        if (target == null)
        {
            // atribui o próprio GameObject como alvo.
            target = gameObject;
            Debug.Log("ChangeLookAtTarget target not specified. Defaulting to parent GameObject");
        }
        // tenta obter o componente AudioSource do próprio objeto.
        if (!gameObject.TryGetComponent(out music))
            target.TryGetComponent(out music);
    }

    // O método 'OnMouseDown' é chamado quando o usuário clica com o mouse no objeto ao qual este script está anexado.
    void OnMouseDown()
    {
        LookAtTarget.target = target;

        // verifica se há uma fonte de áudio atribuída.
        if (music != null)
        {
            // se houver, chama o MusicManager para tocar a música,
            MusicManager.Instance.PlayMusic(music, 2f);
        }

        Camera.main.transform.position = target.transform.position - (Camera.main.transform.forward * cameraDistance);
    }

}
