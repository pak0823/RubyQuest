using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // ui �̹����� �ǵ帮������ ����
using UnityEngine.SceneManagement; // �� �̵� ����Ҷ� ����

public class Fade_img : MonoBehaviour
{
    public GameObject img_obj; //ȿ�� ������ ������Ʈ�� ���� ����
    public Image img;  //�̹��� ������Ʈ�� ���� �̹��� ����
    public GameObject LoadingImage;
    public float fadeSpeed = 0.5f; //Fade in/out �ӵ�

    string currentSceneName; //���� ���̸��� �����ϴ� ����
    public SoundManager sm;
    public bool END = false;

    // Start is called before the first frame update
    void Start()
    {
        sm = Shared.soundMgr;
        img = img_obj.GetComponent<Image>(); //img ������ ȿ�� ������ ������Ʈ�� Image ������Ʈ ������
        currentSceneName = SceneManager.GetActiveScene().name; //���� �� �̸��� �ҷ���
        if (!END)
        {
            CallFadeOut();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CallFadeIn() // Fade In ȿ��
    {
        img_obj.SetActive(true); //�̹����� SetActive �մϴ� �ߺ� Ŭ�� ����
        sm.Loading();
        StartCoroutine(FadeIn()); //Fade in �ڷ�ƾ ����
    }

    public void CallFadeOut() // Fade In ȿ��
    {
        StartCoroutine(FadeOut()); //Fade in �ڷ�ƾ ����
    }

    IEnumerator FadeIn() //Fade in ȿ��
    {
        img.color = new Color32(0, 0, 0, 0);
        // 0.5�� ��ٷȴ� ����
        yield return new WaitForSeconds(0.5f);

            // Fade in
            float t = 0f; //0%
            while (t < 1f) //100%����
            {
                t += Time.deltaTime * fadeSpeed; //������ * ȿ���ӵ��� ���ؼ� % �����Ȳ�� ���մϴ�
                Color color = img.color; //���� �̹����� �� ������Ʈ�� ������

                //�̹����� ������a alpha ���� �������� (1���Լ��� ���۰�, ����, ���������) �Ű������� �����Ȳ�� �־� ���İ��� �����
                color.a = Mathf.Lerp(0f, 1f, t); 
            

                img.color = color; // ����
                yield return null; //�̰� ������ ���İ��� ȿ���� ������
            }
        gameObject.SetActive(false);
        LoadingImage.SetActive(true);
    }

    IEnumerator FadeOut() //Fade Out ȿ��
    {
        // 0.5�� ��ٷȴ� ����
        yield return new WaitForSeconds(0.5f);

        // Fade Out
        float t = 0f; //0%
        while (t < 1f) //100%����
        {
            t += Time.deltaTime * fadeSpeed; //������ * ȿ���ӵ��� ���ؼ� % �����Ȳ�� ���մϴ�
            Color color = img.color; //���� �̹����� �� ������Ʈ�� ������

            //�̹����� ������a alpha ���� �������� (1���Լ��� ���۰�, ����, ���������) �Ű������� �����Ȳ�� �־� ���İ��� �����
            color.a = Mathf.Lerp(1f, 0f, t);


            img.color = color; // ����
            yield return null; //�̰� ������ ���İ��� ȿ���� ������
        }
        gameObject.SetActive(false);
        img.color = new Color32(0, 0, 0, 255);
    }

    public void thisFade()
    {
        gameObject.SetActive(true);
        StartCoroutine(ThisFadeIn()); //Fade in �ڷ�ƾ ����
    }

    IEnumerator ThisFadeIn() //Fade in ȿ��
    {
        img.color = new Color32(0, 0, 0, 0);
        // 0.5�� ��ٷȴ� ����
        yield return new WaitForSeconds(0.5f);

        // Fade in
        float t = 0f; //0%
        while (t < 1f) //100%����
        {
            t += Time.deltaTime * fadeSpeed; //������ * ȿ���ӵ��� ���ؼ� % �����Ȳ�� ���մϴ�
            Color color = img.color; //���� �̹����� �� ������Ʈ�� ������

            //�̹����� ������a alpha ���� �������� (1���Լ��� ���۰�, ����, ���������) �Ű������� �����Ȳ�� �־� ���İ��� �����
            color.a = Mathf.Lerp(0f, 1f, t);


            img.color = color; // ����
            yield return null; //�̰� ������ ���İ��� ȿ���� ������
        }
    }
}