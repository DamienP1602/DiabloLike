using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Throw Spell", menuName = "Spell/Create new Throwing Spell")]
public class ThrowSpell : Spell
{
    [Header("Specific Spell Informations")]
    public ProjectileComponent projectile;
    public float lifespan = 3.0f;

    public override void Execute(Player _owner, Transform _spellSocket)
    {
        ProjectileComponent _projectile = Instantiate(projectile, _spellSocket.transform.position,_owner.transform.rotation);
        _projectile.SetOwner(_owner);

        CameraComponent _cameraOwnerComp = _owner.GetComponent<CameraComponent>();

        Ray _ray = _cameraOwnerComp.RenderCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit _result;
        if (Physics.Raycast(_ray, out _result, 100.0f, _owner.GetComponent<ClickComponent>().EnemyLayer))
        {
            Vector3 _lookAt = _result.point - _projectile.transform.position;
            if (_lookAt == Vector3.zero) return;

            Quaternion _rot = Quaternion.LookRotation(_lookAt);
            _projectile.transform.rotation = _rot;
        }

        Destroy(_projectile, lifespan);
    }
}
