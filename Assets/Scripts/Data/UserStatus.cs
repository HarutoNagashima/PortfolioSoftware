using System;
using System.Reflection;

[Serializable]
public class UserStatus : ICloneable
{
    // Field
    public int UserLv;
    public int Stamina;
    public int Coin;
    public int Dia;

    public ICloneable Clone()
    {
        UserStatus cloneData = new UserStatus();
        cloneData.UserLv = this.UserLv;
        cloneData.Stamina = this.Stamina;
        cloneData.Coin = this.Coin;
        cloneData.Dia = this.Dia;

        /*
        FieldInfo[] fields = typeof(UserStatus).GetFields();
        foreach (FieldInfo field in fields) {
            field.SetValue(cloneData, field.GetValue(this));
        }
        */

        return cloneData;
    }

    public void UpdateData(UserStatus data)
    {
        FieldInfo[] fields = typeof(UserStatus).GetFields();
        foreach (FieldInfo field in fields)
        {
            field.SetValue(this, field.GetValue(data));
        }
    }
}

public interface ICloneable 
{
    ICloneable Clone();
}
