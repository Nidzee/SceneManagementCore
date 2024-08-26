




//////////////////////////////////////////////////
// This class is transmitter betweeen scenes
// It lives in project-scope
//////////////////////////////////////////////////
public class GameSceneTransferingData
{
    public int SomeData;



    public void PutSomeData(int someData)
    {
        SomeData = someData;
    }

    public int GetSOmeData()
    {
        return SomeData;
    }
}