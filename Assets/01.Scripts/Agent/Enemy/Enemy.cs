﻿using System;
using ItemManage;
using UnityEngine;


namespace EnemyManage
{
    public abstract class Enemy : Agent
    {
        [Header("Drop Item settings")]
        [SerializeField] private ItemType _itemType;
        [SerializeField] private int _id;
        
        [Header("Layer Settings")]
        [SerializeField] protected LayerMask _whatIsPlayer;
        [SerializeField] protected LayerMask _whatIsObstacle;
        [field:SerializeField] public SpriteRenderer RendererCompo { get; protected set; }
        public Rigidbody2D RigidCompo { get; protected set; }
        public Collider2D ColliderCompo { get; protected set; }

        [Header("Attack Settings")] public float runAwayDistance;
        public float attackDistance;
        public float chaseDistance;
        public float attackCooldown;
        protected int _maxCheckEnemy = 1;
        public float lastAttackTime;
        public Transform targetTrm;
        protected Collider2D[] _enemyCheckColliders;
        
        public LayerMask WhatIsPlayer => _whatIsPlayer;

        protected override void Awake()
        {
            base.Awake();
            RigidCompo = GetComponent<Rigidbody2D>();
            ColliderCompo = GetComponent<Collider2D>();
            MovementCompo.Initialize(this);
            _enemyCheckColliders = new Collider2D[_maxCheckEnemy];
        }

        public virtual Collider2D IsPlayerDetected()
        {
            int cnt = Physics2D.OverlapCircleNonAlloc(transform.position, chaseDistance, _enemyCheckColliders,
                _whatIsPlayer);

            return cnt >= 1 ? _enemyCheckColliders[0] : null;
        }

        public virtual bool IsObstacleDetected(float distance, Vector3 direction)
        {
            return Physics2D.Raycast(transform.position, direction, distance, _whatIsObstacle);
        }

        public abstract void AnimationEndTrigger();

        public override void SetDead()
        {
            ItemDropManager.Instance.DropItem(_itemType, _id, transform.position);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackDistance);
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }

        public Vector3 GetRandomPosition()
        {
            Vector3 randomPos =
                new Vector3(UnityEngine.Random.Range(-0.5f, 0.5f), UnityEngine.Random.Range(-0.5f, 0.5f), 0).normalized;
            return transform.position + randomPos;
        }
    }
}