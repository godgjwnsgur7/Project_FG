using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 네트워크 통신이 필요한 MonoBehaviour 클래스는 대신 해당 클래스를 사용하시오. 
/// </summary>
/// 

public class PhotonCustomStreamBase
{
    protected PhotonStream stream;

    public void SetStream(PhotonStream stream)
    {
        this.stream = stream;
    }
}

public class PhotonWriteStream : PhotonCustomStreamBase
{
    public void Write(object data)
    {
        stream.SendNext(data);
    }
}

public class PhotonReadStream : PhotonCustomStreamBase
{
    public object Read()
    {
        return stream.ReceiveNext();
    }
}

public class MonoBehaviourPhoton : MonoBehaviourPun, IPunObservable
{
    private PhotonWriteStream writeStream = new PhotonWriteStream();
    private PhotonReadStream readStream = new PhotonReadStream();

    /// <summary>
    /// 우선 Key를 string으로 지정, 추후 클라 관리를 위해 ENUM으로 수정하도록 함
    /// </summary>

    private Dictionary<string, Animator> syncAnimatorDictionary = new Dictionary<string, Animator>();
    private Dictionary<string, Rigidbody2D> syncRigidbodyDictionary = new Dictionary<string, Rigidbody2D>();
    private Dictionary<string, Transform> syncTransformDictionary = new Dictionary<string, Transform>();

    protected virtual void Awake()
    {
        gameObject.AddComponent<PhotonView>();
    }

    public void SyncAnimator(string key, Animator anim)
    {
        if(syncAnimatorDictionary.ContainsKey(key))
            syncAnimatorDictionary.Add(key, anim);

        GameObject ownerObj = anim.gameObject;

        var component = ownerObj.GetComponent<PhotonAnimatorView>();

        if (component == null)
            component = ownerObj.AddComponent<PhotonAnimatorView>();

        photonView.ObservedComponents.Add(component);
    }

    public void SyncTransformView(string key, Transform tr)
    {
        if (syncTransformDictionary.ContainsKey(key))
            syncTransformDictionary.Add(key, tr);

        GameObject ownerObj = tr.gameObject;

        var component = ownerObj.GetComponent<PhotonTransformView>();

        if (component == null)
            component = ownerObj.AddComponent<PhotonTransformView>();

        photonView.ObservedComponents.Add(component);
    }

    public void SyncPhysics(string key, Rigidbody2D rigid)
    {
        if (syncRigidbodyDictionary.ContainsKey(key))
            syncRigidbodyDictionary.Add(key, rigid);

        GameObject ownerObj = rigid.gameObject;

        var component = ownerObj.GetComponent<PhotonRigidbody2DView>();

        if (component == null)
            component = ownerObj.AddComponent<PhotonRigidbody2DView>();

        photonView.ObservedComponents.Add(component);
    }

    protected virtual void OnEnable()
    {
        PhotonLogicHandler.Register(this);
    }

    protected virtual void OnDisable()
    {
        PhotonLogicHandler.Unregister(this);
    }

    /// <summary>
    /// 마스터 클라이언트가 모든 슬레이브들을 동기화시킬 때 사용하는 함수, OnPhotonSerializeView에 의해 자동 적용되므로 호출할 필요는 없음, 정의만
    /// </summary>
    /// <param name="info"></param>

    protected virtual void OnMasterSerializeView(PhotonWriteStream writeStream)
    {
        
    }

    /// <summary>
    /// 슬레이브 클라이언트가 마스터로부터 받은 변수로 동기화를 받을 때 사용하는 함수, OnPhotonSerializeView에 의해 자동 적용되므로 호출할 필요는 없음, 정의만
    /// </summary>
    /// <param name="info"></param>

    protected virtual void OnSlaveSerializeView(PhotonReadStream readStream)
    {
        
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            writeStream.SetStream(stream);
            OnMasterSerializeView(writeStream);
        }
        else if(stream.IsReading)
        {
            readStream.SetStream(stream);
            OnSlaveSerializeView(readStream);
        }

        Debug.Log("--- OnPhotonSerializeView ---");
        Debug.Log($"정보 보낸 이 : {info.Sender?.NickName}");
        Debug.Log($"보낸 시간 : {info.SentServerTime}");
        Debug.Log($"보낸 시간 스탬프 : {info.SentServerTimestamp}");
        Debug.Log("--- --------------------- ---");
    }
}
