// 22130
// 22153

using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour
{

    public static MusicManager Instance;
    private AudioSource currentMusic;   // armazena a fonte de áudio que está tocando no momento.


    private void Awake()
    {
        // verifica se já existe uma instância do MusicManager.
        if (Instance == null)
        {
            Instance = this;
            // impede que o objeto seja destruído ao carregar uma nova cena, garantindo que a música continue tocando.
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            // se já existe uma instância, destrói este novo objeto para evitar duplicatas.
            Destroy(gameObject);
        }
    }

    // Método público para iniciar a reprodução de uma nova música.
    public void PlayMusic(AudioSource newMusic, float fadeTime = 1f)
    {
        // se a nova música for a mesma que já está tocando, o método é encerrado.
        if (currentMusic == newMusic) return;

        // se houver uma música tocando, inicia a corrotina para fazer o 'fade-out'.
        if (currentMusic != null && currentMusic.isPlaying)
        {
            StartCoroutine(FadeOut(currentMusic, fadeTime));
        }

        // a nova música se torna a música atual.
        currentMusic = newMusic;
        if (currentMusic != null)
        {
            currentMusic.volume = 0;
            currentMusic.Play();
            StartCoroutine(FadeIn(currentMusic, fadeTime));
        }
    }

    // Corrotina para aumentar o volume da música gradualmente (fade-in é o aparecimento gradual).
    private IEnumerator FadeIn(AudioSource audio, float time)
    {
        float targetVolume = 1f;
        // aumenta o volume até atingir o volume máximo.
        while (audio != null && audio.volume < targetVolume)
        {
            audio.volume += Time.deltaTime / time;
            yield return null;
        }
    }

    // Corrotina para diminuir o volume da música gradualmente (fade-out é o desaparecimento gradual).
    private IEnumerator FadeOut(AudioSource audio, float time)
    {
        float startVolume = audio.volume;
        // diminui o volume até atingir zero.
        while (audio != null && audio.volume > 0)
        {
            audio.volume -= startVolume * Time.deltaTime / time;
            yield return null;
        }
        // quando o volume chega a zero, a reprodução é parada.
        if (audio != null) audio.Stop();
    }

}
