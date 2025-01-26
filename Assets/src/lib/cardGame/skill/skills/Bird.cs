using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
public class Bird: SkillBundle {
    public Bird(Card ownerCard) : base(ownerCard) {}
    private class Skill0 : Active {
        public Skill0(Card card) : base(card) {}
        public override int SkillNumber => 0;
        public override int CountOfTarget => 1;
        public override int ConsumableBp => 1;
        public override void SetTarget(Card target) {
            this.Target = target;
        }
        public override void Before() {
            
        }
        public override async Task Act() {
            Vector3 cardPos = Card.gameObject.transform.position;
            GameObject flyingBird = GameObject.Instantiate(
                Resources.Load<GameObject>($"Prefabs/skill/flying_bird"), 
                new Vector3(cardPos.x, cardPos.y, cardPos.z-0.07f), 
                Quaternion.identity
            );
            Camera cam = Cam.Instance.Camera;
            Vector3 TargetPos = Target.gameObject.transform.position;

            Task Sequence1 = DOTween.Sequence().SetAutoKill(false)
                .Append(Card.gameObject.transform.Find("front").Find("cardImage").GetComponent<SpriteRenderer>().DOFade(0, 0))
                .Append(flyingBird.transform.DOMoveY(0.1f, 0.5f))

                .Append(flyingBird.transform.DOMoveX(TargetPos.x/2, 1).SetEase(Ease.OutQuad))
                .Join(flyingBird.transform.DOMoveY(TargetPos.y/2, 1).SetEase(Ease.InQuad))

                .AsyncWaitForCompletion();
            await Sequence1;
            Target.Damage(1, this.IsSameTimeSkill);
            GameObject.Destroy(flyingBird);
            Task Sequence2 = DOTween.Sequence().SetAutoKill(false)
                .Append(Card.gameObject.transform.Find("front").Find("cardImage").GetComponent<SpriteRenderer>().DOFade(1, 0.5f))
                .AsyncWaitForCompletion();
            await Sequence2;
        }
        public override void After() {
            
        }
    }
    private class Skill1: Active {
        public Skill1(Card card) : base(card) {}
        public override int SkillNumber => 1;
        public override int CountOfTarget => 0;
        public override int ConsumableBp => 1;
        public override void SetTarget(Card target) {
            this.Target = target;
        }
        public override void Before() {
            
        }
        public override async Task Act() {
            await Task.Delay(0);
            Card.AddDefence(1);
        }
        public override void After() {
            
        }
    }
    public override List<Active> Actives => new List<Active>() { new Skill0(Card), new Skill1(Card) };
    public override List<Passive> Passives => new List<Passive>() {};
}