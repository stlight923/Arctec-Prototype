using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int sceneBuildIndex = 0;

    [SerializeField] private int maxParticleCount;
    [SerializeField] private int minParticleCount; 

    [SerializeField] private GameObject blade;
    [SerializeField] private GameObject spray;

    [SerializeField] private ParticleSystem _particleSystem;

    [SerializeField] private bool _changeTool;
    [SerializeField] private bool _checkedParticleCount;
    [SerializeField] private bool _usedToolSpray;
    [SerializeField] private bool _usedToolBlade;

    private void Start()
    {
        Cursor.visible = false;
    }

    private void Update()
    {
        CheckParticleCount();
        if (_particleSystem.particleCount > maxParticleCount)
        {
            _changeTool = true;
            _checkedParticleCount = true;
            SelectBlade(true);
            SelectSpray(false);
            _particleSystem.Stop();

        }

        if (_particleSystem.particleCount < maxParticleCount && !_changeTool)
        {
            SelectBlade(false);
            SelectSpray(true);                    
        }
    }

    private void CheckParticleCount()
    {
        if (!_checkedParticleCount) { return; }

        if (_particleSystem.particleCount <= minParticleCount)
        {
            SceneManager.LoadScene(sceneBuildIndex);
        }
    }


    private void SelectSpray(bool state)
    {
        _usedToolSpray = state;
        spray.SetActive(_usedToolSpray);
    }

    private void SelectBlade(bool state)
    {
        _usedToolBlade = state;
        blade.SetActive(_usedToolBlade);

    }
}
