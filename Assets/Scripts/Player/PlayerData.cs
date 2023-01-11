using Firebase.Firestore;

[FirestoreData]
public struct PlayerData 
{
    [FirestoreProperty]
    public int exp { get; set; }
    [FirestoreProperty]
    public float silver { get; set; }

    
}
