using UnityEngine;

public interface IRocketStrategy
{ 
    void Handle();
    void Init(Rigidbody2D  rocketRb);
}