using UnityEngine;

public class AnimationEventsRelay : MonoBehaviour
{
    // Função chamada pela animação de passos
    private void OnFootstep(AnimationEvent animationEvent)
    {
        // Pode deixar vazio para ignorar o evento e sumir com o erro
    }

    // Função chamada pela animação ao aterrar no chão
    private void OnLand(AnimationEvent animationEvent)
    {
        // Pode deixar vazio para ignorar o evento
    }
}