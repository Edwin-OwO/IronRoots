using UnityEngine;
namespace Economia
{
    public interface IEntrySource
    {
        event System.Action<float, SpriteRenderer> OnEntryGenerated;
    }
}