using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Pool 
{
	public GameObject model;
	public int preloadNumber;
	public bool shouldExpand;
	public List<GameObject> objects;

	public Pool(GameObject model_, int preload) 
	{
		model = model_;
		this.preloadNumber = preload;
		shouldExpand = true;

		objects = new List<GameObject>();
		for (int i = 0; i < preload; i++) {
			GameObject obj = GameObject.Instantiate(model_) as GameObject;
			obj.SetActive(false);
			objects.Add(obj);
		}
	}
}

public class ObjectPool : Singleton<ObjectPool> 
{

	public List<Pool> pools;

	// Use this for initialization
	void Start () 
	{
		foreach (Pool pool in pools) {
			pool.objects = new List<GameObject>();

			for (int i = 0; i < pool.preloadNumber; i++) {
				GameObject obj = (GameObject)Instantiate(pool.model);
				obj.SetActive(false);
				pool.objects.Add(obj);
			}
		}
	}

	void Init(GameObject obj, GameObject model)
	{
		obj.transform.position = model.transform.position;
		obj.transform.rotation = model.transform.rotation;
		obj.SetActive(true);
	}

	public Pool NewPool(GameObject model)
	{
		Pool newPool = new Pool(model, 1);
		pools.Add(newPool);
		return newPool;
	}

	public GameObject GetPooledObject(GameObject model) 
	{

		foreach (Pool pool in pools) {
			if (pool.model.GetInstanceID() == model.GetInstanceID()) {

				for (int i = 0; i < pool.objects.Count; i++) {
					if (!pool.objects[i].activeInHierarchy) {
						Init(pool.objects[i], model);
						return pool.objects[i];
					}
				}

				// Pool is empty
				if (pool.shouldExpand) {
					GameObject obj = (GameObject)Instantiate(pool.model);
					pool.objects.Add(obj);
					Init(obj, model);
					return obj;
				}
				// - No object left in the pool and it cant expand
				return null;
			}
		}

		// - No pool of this 'model'
		GameObject o = NewPool(model).objects[0];
		Init(o, model);
		return o;
	}

	public void PushToPool(GameObject obj)
	{
		obj.SetActive(false);
	}
}
