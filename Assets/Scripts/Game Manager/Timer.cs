using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;
    public StatUpgrade statUpgrade;
    float startTime;
    private bool isTimeOut = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ResetTimer(remainingTime);   
    }

    private void Awake()
    {
        startTime = remainingTime;
    }
    // Update is called once per frame
    void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }
        else if (remainingTime <= 0 && !isTimeOut)
        {
            remainingTime = 0;
            isTimeOut = true;
            TimeOut();

        }

        //membagi waktu menjadi detik dan menit
        //Membulatkan angka desimal ke bawah menjadi bilangan bulat (int),
        //sehingga pecahan angka di belakang koma terbuang dan tidak berkedip liar di layar.
        int minutes = Mathf.FloorToInt(remainingTime / 60); //Mengambil jumlah menit penuh (misalnya: 120sec / 60 = 2 mnt).
        int seconds = Mathf.FloorToInt(remainingTime % 60); //(mod / modulus adalah sisa)Mengambil sisa detik setelah dibagi 60 (misalnya: 125 (mod 60) = 5sec).
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void TimeOut()
    {
        statUpgrade.TriggerUpdateStat();
    }

    public void ResetTimer()
    {
        remainingTime = startTime;
        isTimeOut = false;
    }

    public void ResetTimer(float newTime)
    {
        remainingTime = newTime;
        startTime = newTime;
        isTimeOut = false;
    }

}
