using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightSkillBuff : BaseBuff
{
    private List<Binding> Binds = new List<Binding>();
    

    private void Start()
    {
        Binding Bind = BindingManager.Bind(TurnSystem.Property, "IsPlayerTurn", (object value) =>
        {
            if (isActive)
            {
                if (!TurnSystem.Property.IsPlayerTurn)
                {
                    buffTurnCount--;
                    if (buffTurnCount <= 0)
                    {
                        _cdm.m_attackPower -= atkPowerBuff;
                        atkPowerBuff = 0;
                        ActionComplete();
                    }
                }
            }
        });
        Binds.Add(Bind);

        buffTurnCount = 0;
    }

    private void Update()
    {
        if(!isActive)
        {
            return;
        }
    }



    public override void TakeAction(GridPosition gridPosition)
    {
        buffTurnCount = 2;

        _cdm = unit.GetCharacterDataManager();
        atkPowerBuff = (int)((int)_cdm.m_attackPower * 0.3f);
        _cdm.m_attackPower += atkPowerBuff;

        ActionStart();
    }


    private void OnDisable()
    {
        foreach (var bind in Binds)
        {
            BindingManager.Unbind(TurnSystem.Property, bind);
        } 
    }
}
