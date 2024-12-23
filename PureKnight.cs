using HutongGames.PlayMaker.Actions;
using Modding;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vasi;
using System.Collections.Generic;
using HKMirror.Reflection;
using System.Collections;
using HutongGames.PlayMaker;
using UnityEngine.Audio;
using GlobalEnums;

namespace Radiant_Knight
{
    public class Pure_Knight : Mod, IGlobalSettings<Settings>, IMenuMod, IMod, Modding.ILogger
    {
        public override string GetVersion()
        {
            return "0.0.1.0";
        }
        public static Settings settings_ = new Settings();
        public bool ToggleButtonInsideMenu => true;
        public List<IMenuMod.MenuEntry> GetMenuData(IMenuMod.MenuEntry? toggleButtonEntry)
        {
            List<IMenuMod.MenuEntry> menus = new List<IMenuMod.MenuEntry>();
            if (toggleButtonEntry != null)
            {
                menus.Add(toggleButtonEntry.Value);
            }
            menus.Add(new IMenuMod.MenuEntry
            {
                Name = this.OtherLanguage("使用纯粹容器形态。", "Pure Vessel Shape On."),
                Description = this.OtherLanguage("开启或关闭后重新进入存档以生效。", "Re-enter the archive after opening or closing to take effect."),
                Values = new string[]
                {
                    Language.Language.Get("MOH_ON", "MainMenu"),
                    Language.Language.Get("MOH_OFF", "MainMenu")
                },
                Loader = (() => (!settings_.on) ? 1 : 0),
                Saver = delegate (int i)
                {
                    settings_.on = (i == 0);
                }
            });

            return menus;
        }
        public void OnLoadGlobal(Settings settings) => settings_ = settings;
        public Settings OnSaveGlobal() => settings_;
        private string OtherLanguage(string chinese, string english)
        {
            if (Language.Language.CurrentLanguage() == Language.LanguageCode.ZH)
            {
                return chinese;
            }
            return english;
        }
        public string ModHooks_LanguageGetHook(string key, string sheet, string text)
        {
            if (settings_.on == true)
            {
                if(text.Contains("Dream"))
                {
                    Modding.Logger.Log(key);
                }
                if(text.Contains("dream"))
                {
                    Modding.Logger.Log(key);
                }
                if (key == "INV_DESC_DREAMNAIL_A")
                {
                    if (Language.Language.CurrentLanguage() == Language.LanguageCode.ZH)
                    {
                        text += "\n\n[纯粹之形]\n如果受伤，已有的神圣力量层数清零。\n满层神圣力量时，可以点按梦之钉消耗所有力量为下次体术充能，若下次格挡成功/额外冲刺三次及以上/释放卷须且解锁对应绝技，则释放该绝技。";
                    }
                    else
                    {
                        text += "[Pure Shape] If injured, the existing levels of Divine power are eliminated. When full of Divine power, you can tap the Dream Nail to expend all power to recharge the next body art, if the next block is successful/additional sprint three or more times/release the tendrils and unlock the corresponding trick, release the trick.";
                    }
                }
                if (key == "INV_DESC_DREAMNAIL_B")
                {
                    if (Language.Language.CurrentLanguage() == Language.LanguageCode.ZH)
                    {
                        text += "\n\n[纯粹之形]\n如果受伤，已有的神圣力量层数清零。\n满层神圣力量时，可以点按梦之钉消耗所有力量为下次体术充能，若下次格挡成功/额外冲刺三次及以上/释放卷须且解锁对应绝技，则释放该绝技。";
                    }
                    else
                    {
                        text += "[Pure Shape] If injured, the existing levels of Divine power are eliminated. When full of Divine power, you can tap the Dream Nail to expend all power to recharge the next body art, if the next block is successful/additional sprint three or more times/release the tendrils and unlock the corresponding trick, release the trick.";
                    }
                }
                if (key == "INV_DESC_SPELL_FIREBALL2")
                {
                    if (Language.Language.CurrentLanguage() == Language.LanguageCode.ZH)
                    {
                        text = "召唤一把向前飞行的长钉，斩杀路径上的敌人。\n长钉会自行修正飞行方向。\n\n可以被萨满之石增强。";
                    }
                    else
                    {
                        text = "Summon a spike that flies forward to kill enemies in its path. The spike will correct its own direction. Can be enhanced by shaman stone.";
                    }
                }
                if (key == "INV_DESC_SPELL_SCREAM2")
                {
                    if (Language.Language.CurrentLanguage() == Language.LanguageCode.ZH)
                    {
                        text = "用灵魂和苍白凝聚成法阵轰击敌人。\n\n可以被萨满之石增强。";
                    }
                    else
                    {
                        text = "Bombard the enemy with the spirit and the pale. \n\nCan be enhanced by shamanic stones.";
                    }
                }
                if (key == "INV_DESC_SPELL_QUAKE2")
                {
                    if (Language.Language.CurrentLanguage() == Language.LanguageCode.ZH)
                    {
                        text = "集中灵魂和苍白的力量打击地面，升起剑锋摧毁敌人或破坏脆弱的结构。\n\n可以被萨满之石增强。";
                    }
                    else
                    {
                        text = "Focus your soul and pale power on striking the ground, raising the blade to destroy enemies or damage fragile structures. Can be enhanced by shamanic stones.";
                    }
                }
                if (key == "INV_DESC_SPELL_FOCUS")
                {
                    if (Language.Language.CurrentLanguage() == Language.LanguageCode.ZH)
                    {
                        text += "\n\n聚集完成后会产生对敌人造成伤害的法阵。\n\n可以被深度聚集增强。\n可以被蜕变挽歌增强。";
                    }
                    else
                    {
                        text += "When the focusing is complete, it creates a spell that deals damage to enemies. \n\n Can be enhanced by Deep Focus. \nCan be enhanced by Grubberfly's Elegy.";
                    }
                }
                if (key == "INV_NAME_SPELL_FIREBALL2")
                {
                    if (Language.Language.CurrentLanguage() == Language.LanguageCode.ZH)
                    {
                        text = "苍白之剑";
                    }
                    else
                    {
                        text = "Pale Nail";
                    }
                }
                if (key == "INV_NAME_SPELL_SCREAM2")
                {
                    if (Language.Language.CurrentLanguage() == Language.LanguageCode.ZH)
                    {
                        text = "苍白法阵";
                    }
                    else
                    {
                        text = "Pale Matrix";
                    }
                }
                if (key == "INV_NAME_SPELL_QUAKE2")
                {
                    if (Language.Language.CurrentLanguage() == Language.LanguageCode.ZH)
                    {
                        text = "苍白俯冲";
                    }
                    else
                    {
                        text = "Pale Dive";
                    }
                }
                if (key == "INV_NAME_ART_DASH")
                {
                    if (Language.Language.CurrentLanguage() == Language.LanguageCode.ZH)
                    {
                        text = "格挡反击";
                    }
                    else
                    {
                        text = "Block counter";
                    }
                }
                if (key == "INV_NAME_ART_UPPER")
                {
                    if (Language.Language.CurrentLanguage() == Language.LanguageCode.ZH)
                    {
                        text = "暗影瞬身";
                    }
                    else
                    {
                        text = "Shadow instants";
                    }
                }
                if (key == "INV_NAME_ART_CYCLONE")
                {
                    if (Language.Language.CurrentLanguage() == Language.LanguageCode.ZH)
                    {
                        text = "虚空之击";
                    }
                    else
                    {
                        text = "Void strike";
                    }
                }
                if (key == "INV_DESC_ART_UPPER")
                {
                    if (Language.Language.CurrentLanguage() == Language.LanguageCode.ZH)
                    {
                        text = "[纯粹之形]\n向前冲刺后在闪光时间内可再次进行暗影冲刺，最多再冲刺三次。\n佩戴锋利之影时，每三次冲刺命中获得一层神圣力量。\n\n可以被冲刺大师增强。\n可以被锋利之影增强。\n可以被蜕变挽歌增强。\n可以被亡者之怒增强。";
                    }
                    else
                    {
                        text = "[Pure Shape]\n After the dash, can perform the shadow dash again within the flash time, up to three more dashes. Can be enhanced by Dashmaster. Can be enhanced by Grubberfly's Elegy.";
                    }
                }
                if (key == "INV_DESC_ART_DASH")
                {
                    if (Language.Language.CurrentLanguage() == Language.LanguageCode.ZH)
                    {
                        text = "[纯粹之形]\n产生瞬间的格挡效果，可以抵御伤害，如果格挡成功，会立刻进行大威力的反击。\n如果格挡时间结束，也会进行较弱的挥砍。\n\n格挡成功后，获得一层神圣力量。\n\n可以被沉重之击增强。\n可以被蜕变挽歌增强。\n可以被亡者之怒增强。";
                    }
                    else
                    {
                        text = "[Pure Shape]\n Creates an instant block effect that can resist damage, and if the block is successful, it will immediately launch a powerful counterattack. If the block time ends, a weaker slash will also be performed. Can be enhanced by Heavy Blow. Can be enhanced by Grubberfly's Elegy. Can be enhanced by the Fury of the Fallen.";
                    }
                }
                if (key == "INV_DESC_ART_CYCLONE")
                {
                    if (Language.Language.CurrentLanguage() == Language.LanguageCode.ZH)
                    {
                        text = "[纯粹之形]\n消耗一层神圣力量，伸出虚空卷须对敌人造成伤害。当前神圣力量层数越高，虚空卷须越强大。\n\n可以被锋利之影增强。\n可以被蜕变挽歌增强。";
                    }
                    else
                    {
                        text = "[Pure Shape]\n Extends the Void tendrils to deal damage to enemies. \n\n can be enhanced by Sharp shadow. Can be enhanced by Grubberfly's Elegy.";
                    }
                }
                if (key == "CHARM_DESC_15")
                {
                    if (Language.Language.CurrentLanguage() == Language.LanguageCode.ZH)
                    {
                        text += "\n\n[纯粹之形]\n增加格挡反击的时机，但会禁用普通攻击。\n\n获得格挡反击的绝技。";
                    }
                    else
                    {
                        text += "\n\n[Pure Shape]\n Increases the time to block counterattacks, but disables normal attacks.\n\n Gain the ability to block counterattacks.";
                    }
                }
                if (key == "CHARM_DESC_16")
                {
                    if (Language.Language.CurrentLanguage() == Language.LanguageCode.ZH)
                    {
                        text = "当使用暗影冲刺时，持有者的身体会变得锋利，并伤害敌人。\n\n[纯粹之形]\n增加虚空之击的伤害。\n\n获得虚空之击的绝技。";
                    }
                    else
                    {
                        text = "When using Shadow Sprint, the holder's body becomes sharp and damages enemies.\n\n[Pure Form]\n Increases the damage of Void Strike. \n\nGain the magic of the Void Strike.";
                    }
                }
                if (key == "CHARM_DESC_35")
                {
                    if (Language.Language.CurrentLanguage() == Language.LanguageCode.ZH)
                    {
                        text = "包含将要步入生命的下一个阶段的幼虫的感激。为武器灌输神圣的力量。\n\n[纯粹之形]\n所有体术命中时增加法术效果，但体术的所有伤害降低。\n\n非满血聚集后生成额外的小型法阵。";
                    }
                    else
                    {
                        text = "Contains the gratitude of a larva that is about to move on to the next stage of its life. Imbue weapons with divine power. \n\n[Pure Shape]\n Increased spell effect on all somatology, but reduced damages of all somatology. Non-full blood focus to generate additional mini-matrices.";
                    }
                }
                if (key == "CHARM_DESC_6")
                {
                    if (Language.Language.CurrentLanguage() == Language.LanguageCode.ZH)
                    {
                        text += "\n\n[纯粹之形]\n当接近死亡时，格挡反击的绝技会多释放一次。\n增加锋利之影冲刺的伤害。";
                    }
                    else
                    {
                        text += "\n\n[Pure Shape]When close to death, the stunt of blocking counterattack will be released once more. \n Increases the damage of Sharp Shadow Sprint.";
                    }
                }
                if (key == "CHARM_DESC_31")
                {
                    if (Language.Language.CurrentLanguage() == Language.LanguageCode.ZH)
                    {
                        text += "\n\n[纯粹之形]\n暗影瞬身的最大连续冲刺次数提高到五次，并且可以向上冲刺。\n\n获得暗影瞬身的绝技。";
                    }
                    else
                    {
                        text += "\n\n[Pure Shape]\n The maximum number of consecutive sprints of Shadow instant has been increased to five, and can be sprinted upward. Get the magic of Shadow Blink.";
                    }
                }
                if (key == "CHARM_DESC_34")
                {
                    if (Language.Language.CurrentLanguage() == Language.LanguageCode.ZH)
                    {
                        text += "\n\n[纯粹之形]\n增强聚集形成的法阵。";
                    }
                    else
                    {
                        text += "\n\n[Pure Shape]\n Enhance the matrix of focus.";
                    }
                }
                if (key == "CHARM_DESC_19")
                {
                    if (Language.Language.CurrentLanguage() == Language.LanguageCode.ZH)
                    {
                        text += "\n\n[纯粹之形]\n改变法术的形态，长按快速施法继续获得强化。";
                    }
                    else
                    {
                        text += "\n\n[Pure Shape]\n Change the form of the spell, long press quick cast to continue to gain power.";
                    }
                }
                if (key == "CHARM_DESC_33")
                {
                    if (Language.Language.CurrentLanguage() == Language.LanguageCode.ZH)
                    {
                        text += "\n\n[纯粹之形]\n所有攻击都可以获得灵魂，但单次获取量降低。";
                    }
                    else
                    {
                        text += "\n\n[Pure Shape]\n All attacks can gain souls, but the single gain is reduced.";
                    }
                }
                if (key == "CHARM_DESC_26")
                {
                    if (Language.Language.CurrentLanguage() == Language.LanguageCode.ZH)
                    {
                        text = "[纯粹之形]\n减少体术的蓄力时间。";
                    }
                    else
                    {
                        text = "[Pure form]\n Reduces the accumulation time of somatology.";
                    }
                }
                if (key == "CHARM_DESC_21")
                {
                    if (Language.Language.CurrentLanguage() == Language.LanguageCode.ZH)
                    {
                        text += "\n\n[纯粹之形]\n切换场景后会获得33灵魂。";
                    }
                    else
                    {
                        text += "\n\n[Pure Form]\n Gain 33 souls after switching scenes.";
                    }
                }
                return text;
            }
            else
            {
                return @text;
            }
        }
        public static double RadiansToDegrees(double radians)
        {
            return radians * (180 / Math.PI);
        }
        public static double DegreesToRadians(double Degrees)
        {
            return Degrees * (Math.PI / 180);
        }
        public static GameObject BEAM;
        public static GameObject NAIL;
        public static GameObject GLOW;
        public static GameObject NAIL1;
        public static GameObject BLAST;
        public static GameObject BIGBLAST;
        public static GameObject BIGBLASTHIT;
        public static GameObject PLUME;
        public static GameObject DREAMPT;
        public static GameObject TENTRILS;
        public static GameObject WAVE;
        public static GameObject PT;
        public static GameObject PT2;
        public static AudioClip BALLUP;
        public static AudioClip BALLEXPLODE;
        public static AudioClip BLOCKING;
        public static AudioClip BLOCKSUCCEED;
        public static AudioClip NAILART;
        public static AudioClip TELE;
        public static AudioClip PLUMEUP;
        public static AudioClip BLASTEXPLODE;
        public static List<GameObject> Nails = new List<GameObject>();

        public static bool TestFlag = false;
        public static bool SlashAngleReverse = false;
        public static bool GettingHit = false;
        public static bool Blocking = false;
        public static bool BlockSucceed = false;
        public static bool RevengeEnhance = false;
        public static bool SuperNailCharged = false;
        public static bool DashDashing = false;
        public static bool CanSlash = false;
        public static bool Dashing_S = false;
        public static bool LastOnGround = false;
        public static bool CanFireBall = false;
        public static bool CanQuake = false;
        public static bool CanScream = false;
        public static bool CanSuperFireBall = false;
        public static bool CanSuperQuake = false;
        public static bool CanSuperScream = false;
        public static int NailType = 0;
        public static int ShadowDashHitCount = 0;
        public static int NailChargeCount = 0;
        public static Vector2 DashVec = new Vector2(0f, 0f);
        public static Vector3 PositionBeforeDashS;
        bool dashingUp = false;

        public static DamageEnemies SetDamageEnemy(GameObject go, int value = 0, float angle = 0f, float magnitudeMult = 0f, AttackTypes type = AttackTypes.Nail)
        {
            DamageEnemies damageEnemies = go.GetComponent<DamageEnemies>() ?? go.AddComponent<DamageEnemies>();
            damageEnemies.attackType = type;
            damageEnemies.circleDirection = false;
            damageEnemies.damageDealt = value;
            damageEnemies.direction = angle;
            damageEnemies.ignoreInvuln = false;
            damageEnemies.magnitudeMult = magnitudeMult;
            damageEnemies.moveDirection = false;
            damageEnemies.specialType = SpecialTypes.None;
            return damageEnemies;
        }

        public class SceneSwitchDetector : MonoBehaviour
        {
            bool chargeCoolDown = true;
            public void Awake()
            {
                UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
            }
            void OnEnable()
            {
                UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
            }
            void OnDisable()
            {
                UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
            }
            void OnSceneLoaded(Scene scene, LoadSceneMode mode)
            {
                Nails.Clear();
                if (HeroController.instance.playerData.GetBool("equippedCharm_21") && chargeCoolDown)
                {
                    chargeCoolDown = false;
                    Invoke("ChargeCoolDown", 1f);
                    HeroController.instance.gameObject.GetComponent<MPCharge>().ChargeStart();
                }
            }
            void ChargeCoolDown()
            {
                chargeCoolDown = true;
            }
        }
        public class MPCharge : MonoBehaviour
        {
            public void ChargeStart()
            {
                int count = 0;
                int @int = HeroController.instance.playerData.GetInt("MPReserve");
                StartCoroutine(DelayLoopCoroutine());
                IEnumerator DelayLoopCoroutine()
                {
                    while (count < 33 && HeroController.instance.playerData.GetInt("MPCharge") < 198)
                    {
                        HeroController.instance.playerData.AddMPCharge(1);
                        GameCameras.instance.soulOrbFSM.SendEvent("MP GAIN");
                        if (HeroController.instance.playerData.GetInt("MPReserve") != @int)
                        {
                            HeroController.instance.Reflect().gm.soulVessel_fsm.SendEvent("MP RESERVE UP");
                        }
                        count++;
                        yield return new WaitForSeconds(0.025f);
                    }
                }
            }
        }
        public class Cleaner : MonoBehaviour
        {
            void Clean()
            {
                GameObject[] allObjects = FindObjectsOfType<GameObject>();

                foreach (GameObject obj in allObjects)
                {
                    if(obj.name.Contains("Clone") && obj.activeSelf == false && obj.transform.parent == null)
                    {
                        obj.Recycle();
                        obj.tag = "WaitForClean";
                    }
                }
                Invoke("Clean", 3f);
            }
        }
        public class NailChaser1 : MonoBehaviour
        {
            Vector2 ADDING = new Vector2(0f, 0f);
            void Update()
            {
                GameObject closest = null;
                float closestDistance = Mathf.Infinity;

                foreach (GameObject obj in GameObject.FindObjectsOfType<GameObject>())
                {
                    if (obj != gameObject && obj != HeroController.instance.gameObject && obj.GetComponent<HealthManager>() != null && !obj.name.Contains("Zote Balloon"))
                    {
                        float distance = Vector2.Distance(gameObject.transform.position, obj.transform.position);

                        if (distance < closestDistance)
                        {
                            closest = obj;
                            closestDistance = distance;
                        }
                    }
                }
                float speed = (Vector2.Distance(gameObject.GetComponent<Rigidbody2D>().velocity, new Vector2(0, 0)));
                gameObject.transform.localScale = new Vector3(0.8f, 0.8f + speed * 0.008f, 1f);
                if (closest != null)
                {
                    //double angle = Math.Atan((closest.transform.position.y - gameObject.transform.position.y) / (closest.transform.position.x - gameObject.transform.position.x));
                    float distance = Vector2.Distance(gameObject.transform.position, closest.transform.position);
                    if (distance <= 1.5f)
                    {
                        float XADDING = (closest.transform.position.x - gameObject.transform.position.x) * 1100f / distance;
                        float YADDING = (closest.transform.position.y - gameObject.transform.position.y) * 1100f / distance;
                        ADDING = new Vector2(XADDING, YADDING);
                        gameObject.GetComponent<Rigidbody2D>().velocity += (gameObject.GetComponent<Rigidbody2D>().velocity * 24f * Time.deltaTime);
                    }
                    else
                    {
                        float XADDING = (closest.transform.position.x - gameObject.transform.position.x) * 1100f / distance;
                        float YADDING = (closest.transform.position.y - gameObject.transform.position.y) * 1100f / distance;
                        ADDING = new Vector2(XADDING, YADDING);
                        gameObject.GetComponent<Rigidbody2D>().velocity -= (gameObject.GetComponent<Rigidbody2D>().velocity * 6.5f * Time.deltaTime);
                    }
                    gameObject.GetComponent<Rigidbody2D>().velocity += ADDING * Time.deltaTime;
                    SetDamageEnemy(gameObject, 40, gameObject.transform.eulerAngles.z + 90f, speed / 125f);
                }
            }
        }
        public class NailChaser : MonoBehaviour
        {
            Vector2 ADDING = new Vector2(0f, 0f);
            void Update()
            {
                GameObject closest = null;
                float closestDistance = Mathf.Infinity;

                foreach (GameObject obj in GameObject.FindObjectsOfType<GameObject>())
                {
                    if (obj != gameObject && obj != HeroController.instance.gameObject && obj.GetComponent<HealthManager>() != null && !obj.name.Contains("Zote Balloon"))
                    {
                        float distance = Vector2.Distance(gameObject.transform.position, obj.transform.position);

                        if (distance < closestDistance)
                        {
                            closest = obj;
                            closestDistance = distance;
                        }
                    }
                }
                float speed = (Vector2.Distance(gameObject.GetComponent<Rigidbody2D>().velocity, new Vector2(0, 0)));
                gameObject.transform.localScale = new Vector3(0.8f, 0.8f + speed * 0.008f, 1f);
                if (closest != null)
                {
                    double angle = RadiansToDegrees(Math.Atan((closest.transform.position.y - gameObject.transform.position.y) / (closest.transform.position.x - gameObject.transform.position.x)));
                    float distance = Vector2.Distance(gameObject.transform.position, closest.transform.position);
                    if (distance <= 1.5f)
                    {
                        float XADDING = (closest.transform.position.x - gameObject.transform.position.x) * 900f / distance;
                        float YADDING = (closest.transform.position.y - gameObject.transform.position.y) * 900f / distance;
                        ADDING = new Vector2(XADDING, YADDING);
                        gameObject.GetComponent<Rigidbody2D>().velocity += (gameObject.GetComponent<Rigidbody2D>().velocity * 24f * Time.deltaTime);
                    }
                    else
                    {
                        float XADDING = (closest.transform.position.x - gameObject.transform.position.x) * 900f / distance;
                        float YADDING = (closest.transform.position.y - gameObject.transform.position.y) * 900f / distance;
                        ADDING = new Vector2(XADDING, YADDING);
                        gameObject.GetComponent<Rigidbody2D>().velocity -= (gameObject.GetComponent<Rigidbody2D>().velocity * 5.5f * Time.deltaTime);
                    }
                    gameObject.GetComponent<Rigidbody2D>().velocity += ADDING * Time.deltaTime;
                    SetDamageEnemy(gameObject, 20 + Nails.Count * 5, (float)angle, speed / 150f, AttackTypes.Nail);
                }
            }
        }
        public class NailRecycle1 : MonoBehaviour
        {
            public float Timer = 0.2f;
            public void FixedUpdate()
            {
                Timer -= Time.deltaTime;
                if (Timer <= 0.1f)
                {
                    gameObject.transform.localScale /= (1 + 40 * Time.deltaTime);
                }
                if (Timer <= 0)
                {
                    var pt = gameObject.transform.Find("Dribble L").gameObject;
                    pt.transform.SetParent(null);
                    pt.AddComponent<PtRecycle>();
                    pt.GetComponent<ParticleSystem>().emissionRate = 0f;
                    gameObject.Recycle();
                }
            }
        }
        public class NailRecycle : MonoBehaviour
        {
            public float Timer = 1.4f;
            public void FixedUpdate()
            {
                Timer -= Time.deltaTime;
                if (Timer <= 0.2f)
                {
                    gameObject.transform.localScale /= (1 + 20 * Time.deltaTime);
                }
                if (Timer <= 0)
                {
                    if (Nails.Remove(gameObject))
                    {
                        var pt = gameObject.transform.Find("Dribble L").gameObject;
                        pt.transform.SetParent(null);
                        pt.AddComponent<PtRecycle>();
                        pt.GetComponent<ParticleSystem>().emissionRate = 0f;
                        gameObject.Recycle();
                    }

                }
            }
        }
        public class SoonRecycle : MonoBehaviour
        {
            public float Timer = 5f;
            public void FixedUpdate()
            {
                Timer -= Time.deltaTime;
                if (Timer <= 0)
                    gameObject.Recycle();
            }
        }
        public class PtRecycle : MonoBehaviour
        {
            public float Timer = 5f;
            public void FixedUpdate()
            {
                Timer -= Time.deltaTime;
                if (Timer <= 0)
                    gameObject.Recycle();
            }
        }
        public class NailPt : MonoBehaviour
        {
            public float Timer = 5f;
            public float TimerLimit = 5f;
            public void FixedUpdate()
            {
                if (Timer >= 0)
                {
                    Timer -= Time.deltaTime;

                    if (Timer <= TimerLimit)
                    {
                        TimerLimit -= 0.02f;
                        var Pt = GameObject.Instantiate(DREAMPT, gameObject.transform.position + new Vector3(0, 0, 0), Quaternion.Euler(gameObject.transform.eulerAngles + new Vector3(0, 0, 180f)));
                        Pt.gameObject.GetComponent<Transform>().localScale = Pt.gameObject.GetComponent<Transform>().localScale + new Vector3(-0.9f, 0, 0);
                        Pt.gameObject.GetComponent<ParticleSystem>().emissionRate = 10f;
                        Pt.gameObject.GetComponent<ParticleSystem>().startSpeed = 70f;
                        Pt.gameObject.GetComponent<ParticleSystem>().Play();
                        Pt.AddComponent<PtRecycle>();
                    }
                }
            }
        }
        public class BlastSpeed1 : MonoBehaviour
        {
            void Update()
            {
                if(gameObject.transform.Find("hero_damager").GetComponent<CircleCollider2D>().enabled == true)
                {
                    gameObject.GetComponent<Animator>().speed = 0.5f;
                    HeroController.instance.gameObject.GetComponent<AudioSource>().PlayOneShot(BLASTEXPLODE, 1f);
                }
            }
        }
        public class BlastSpeed2 : MonoBehaviour
        {
            void Update()
            {
                if(gameObject.transform.Find("hero_damager").GetComponent<CircleCollider2D>().enabled == true)
                {
                    gameObject.GetComponent<Animator>().speed = 1f;
                    HeroController.instance.gameObject.GetComponent<AudioSource>().PlayOneShot(BLASTEXPLODE, 1f);
                }
            }
        }
        public class BlastSkill1 : MonoBehaviour
        {
            public void Blast()
            {
                Modding.Logger.Log("Blast");
                var blast = GameObject.Instantiate(BLAST, HeroController.instance.gameObject.transform.position + new Vector3(0f, 6.5f, 0), Quaternion.Euler(0f,0f,0f));
                blast.AddComponent<SoonRecycle>();
                blast.tag = "WaitForClean";
                var blastchild = blast.transform.Find("Blast").gameObject;
                blastchild.transform.localScale = new Vector3(1.2f, 1.2f, 1f);
                blastchild.transform.Find("hero_damager").GetComponent<DamageHero>().damageDealt = 0;
                blastchild.transform.Find("hero_damager").gameObject.layer = LayerMask.NameToLayer("Attack");
                blastchild.AddComponent<BlastSpeed1>();
                blastchild.GetComponent<Animator>().speed = 3;
                blastchild.SetActive(true);
                SetDamageEnemy(blastchild.transform.Find("hero_damager").gameObject, 40,90f + HeroController.instance.gameObject.transform.localScale.x * 90f , 1f, AttackTypes.Spell);
            }
        }
        public class BlastSkill2 : MonoBehaviour
        {
            static System.Random random = new System.Random();
            static double R => random.NextDouble();
            static double R1 => random.NextDouble();
            float Timer = 0f;
            float TimerLimit = 0f;
            double angle = R * 2d * Math.PI;
            float R2 = (float)(R1 - 0.5d) * 1.5f;
            int Count = 0;
            public void FixedUpdate()
            {
                if(CanScream && CanSuperScream)
                {
                    BlastOneUpdate(4f, 90, true, 1.5f);
                }
                else if(CanScream)
                {
                    BlastOneUpdate(-0.5f, 50, false, 1.1f);
                }
                else
                {
                    BlastOneUpdate(1.5f, 50, false, 1f);
                }
            }
            void BlastOneUpdate(float Rfactor, int damage, bool Round, float scaleR)
            {
                if (Timer >= 0)
                {
                    Timer -= Time.deltaTime;
                    if (Timer <= TimerLimit)
                    {
                        if(Round)
                        {
                            TimerLimit -= 0.1f;

                            var blast = GameObject.Instantiate(BLAST, HeroController.instance.gameObject.transform.position + new Vector3((float)Math.Cos(angle) * (1.5f + R2), (float)Math.Sin(angle) * (2f + R2) + 6.5f, 0), Quaternion.Euler(0f, 0f, 0f));
                            blast.tag = "WaitForClean";
                            blast.AddComponent<SoonRecycle>();
                            var blastchild = blast.transform.Find("Blast").gameObject;
                            blastchild.transform.localScale = new Vector3(scaleR, scaleR, 1f);
                            blastchild.transform.Find("hero_damager").GetComponent<DamageHero>().damageDealt = 0;
                            blastchild.transform.Find("hero_damager").gameObject.layer = LayerMask.NameToLayer("Attack");
                            blastchild.AddComponent<BlastSpeed2>();
                            blastchild.GetComponent<Animator>().speed = 5;
                            blastchild.SetActive(true);
                            float d = 0f;
                            if ((blast.transform.position.x - HeroController.instance.gameObject.transform.position.x) <= 0)
                            {
                                d = 0f;
                            }
                            else
                            {
                                d = 180f;
                            }
                            SetDamageEnemy(blastchild.transform.Find("hero_damager").gameObject, damage, d, 1f, AttackTypes.Spell);
                            angle += HeroController.instance.gameObject.transform.localScale.x * DegreesToRadians(180f);
                            if (Count == 1 || Count == 3 || Count == 5 || Count == 7 || Count == 9 || Count == 11)
                                angle += HeroController.instance.gameObject.transform.localScale.x * DegreesToRadians(60f);
                            Count += 1;
                            R2 = (float)(R1 - 0.5d) * 1.5f + Rfactor;
                        }
                        else
                        {
                            TimerLimit -= 0.1f;

                            var blast = GameObject.Instantiate(BLAST, HeroController.instance.gameObject.transform.position + new Vector3((float)Math.Cos(angle) * (1.5f + R2), (float)Math.Sin(angle) * (2f + R2) + 6.5f, 0), Quaternion.Euler(0f, 0f, 0f));
                            blast.tag = "WaitForClean";
                            blast.AddComponent<SoonRecycle>();
                            var blastchild = blast.transform.Find("Blast").gameObject;
                            blastchild.transform.localScale = new Vector3(scaleR, scaleR, 1f);
                            blastchild.transform.Find("hero_damager").GetComponent<DamageHero>().damageDealt = 0;
                            blastchild.transform.Find("hero_damager").gameObject.layer = LayerMask.NameToLayer("Attack");
                            blastchild.AddComponent<BlastSpeed2>();
                            blastchild.GetComponent<Animator>().speed = 5;
                            blastchild.SetActive(true);
                            float d = 0f;
                            if ((blast.transform.position.x - HeroController.instance.gameObject.transform.position.x) <= 0)
                            {
                                d = 0f;
                            }
                            else
                            {
                                d = 180f;
                            }
                            SetDamageEnemy(blastchild.transform.Find("hero_damager").gameObject, damage, d, 1f, AttackTypes.Spell);
                            angle += HeroController.instance.gameObject.transform.localScale.x * DegreesToRadians(180f);
                            if (Count == 1 || Count == 3 || Count == 5 || Count == 7 || Count == 9 || Count == 11)
                                angle += HeroController.instance.gameObject.transform.localScale.x * DegreesToRadians(60f);
                            Count += 1;
                            R2 = (float)(R1 - 0.5d) * 1.5f + Rfactor;
                        }
                    }
                }
            }
            public void Blast()
            {
                angle = (float)(R * 2d * Math.PI);
                Timer = 0.6f;
                TimerLimit = 0.6f;
                Count = 0;
            }
        }
        public class NailSkill1 : MonoBehaviour
        {
            public void R()
            {
                Modding.Logger.Log("R");
                var nail = GameObject.Instantiate(NAIL1, HeroController.instance.gameObject.transform.position + new Vector3(0f, -0.5f, 0), Quaternion.Euler(0f,0f,0f));
                nail.tag = "WaitForClean";
                nail.GetComponent<DamageHero>().damageDealt = 0;
                nail.LocateMyFSM("Control").GetState("Start").AddMethod(() =>
                {
                    nail.transform.Find("Beam").Recycle();
                    nail.transform.Find("Glow").transform.localScale *= 2f;
                    nail.transform.Find("Glow").transform.Rotate(0f, 0f, 90f);
                    nail.transform.Find("Glow").transform.SetParent(null);
                    nail.LocateMyFSM("Control").GetState("Fire").GetAction<SetVelocityAsAngle>().speed.Value = 60f;
                    nail.LocateMyFSM("Control").FsmVariables.FindFsmFloat("Angle").Value = 0f;
                    nail.LocateMyFSM("Control").SendEvent("FINISHED");
                    SetDamageEnemy(nail, 40, 0f, 1f);
                });
                nail.LocateMyFSM("Control").GetState("Fire").AddMethod(() =>
                {
                    StartCoroutine(DelayExecute(0.1f));
                    IEnumerator DelayExecute(float delayInSeconds)
                    {
                        yield return new WaitForSeconds(delayInSeconds);
                        nail.layer = LayerMask.NameToLayer("Attack");
                        nail.transform.localScale = new Vector3(1f, 1.8f, 1f);
                        nail.GetComponent<Rigidbody2D>().velocity = new Vector2(100f, 0);
                        nail.AddComponent<NailChaser1>();
                        nail.AddComponent<NailRecycle1>();
                    }
                });
            }
            public void L()
            {
                Modding.Logger.Log("L");
                var nail = GameObject.Instantiate(NAIL1, HeroController.instance.gameObject.transform.position + new Vector3(0f, -0.5f, 0), Quaternion.Euler(0f, 0f, 180f));
                nail.tag = "WaitForClean";
                nail.GetComponent<DamageHero>().damageDealt = 0;
                nail.LocateMyFSM("Control").GetState("Start").AddMethod(() =>
                {
                    nail.transform.Find("Beam").Recycle();
                    nail.transform.Find("Glow").transform.localScale *= 2f;
                    nail.transform.Find("Glow").transform.Rotate(0f, 0f, 90f);
                    nail.transform.Find("Glow").transform.SetParent(null);
                    nail.LocateMyFSM("Control").GetState("Fire").GetAction<SetVelocityAsAngle>().speed.Value = 60f;
                    nail.LocateMyFSM("Control").FsmVariables.FindFsmFloat("Angle").Value = 0f;
                    nail.LocateMyFSM("Control").SendEvent("FINISHED");
                    SetDamageEnemy(nail, 40, 180f, 1f);
                });
                nail.LocateMyFSM("Control").GetState("Fire").AddMethod(() =>
                {
                    StartCoroutine(DelayExecute(0.1f));
                    IEnumerator DelayExecute(float delayInSeconds)
                    {
                        yield return new WaitForSeconds(delayInSeconds);
                        nail.layer = LayerMask.NameToLayer("Attack");
                        nail.transform.localScale = new Vector3(1f, 1.8f, 1f);
                        nail.GetComponent<Rigidbody2D>().velocity = new Vector2(-100f, 0);
                        nail.AddComponent<NailChaser1>();
                        nail.AddComponent<NailRecycle1>();
                    }
                });
            }
        }
        public class NailSkill2 : MonoBehaviour
        {
            static System.Random random = new System.Random();
            static double R1 => random.NextDouble();
            static float R2 = (float)(R1 - 0.5d) * 170f;
            public void Fire()
            {
                FireOne();
                if(CanFireBall)
                {
                    if (CanSuperFireBall)
                    {
                        Invoke("FireOne", 0.1f);
                        Invoke("FireOne", 0.2f);
                        CanSuperFireBall = false;
                    }
                    else
                    {
                        Invoke("FireOne", 0.2f);
                    }
                }
            }
            void FireOne()
            {
                if (HeroController.instance.gameObject.transform.localScale.x <= 0)
                {
                    R();
                }
                else
                {
                    L();
                }
            }
            public void R()
            {
                Modding.Logger.Log("R");
                R2 = (float)(R1 - 0.5d) * 100f;
                var nail = GameObject.Instantiate(NAIL1, HeroController.instance.gameObject.transform.position + new Vector3(0f, -0.5f, 0), Quaternion.Euler(0f,0f,180f));
                nail.tag = "WaitForClean";
                Nails.Add(nail);
                nail.AddComponent<NailRecycle>();
                nail.GetComponent<DamageHero>().damageDealt = 0;
                nail.LocateMyFSM("Control").GetState("Start").AddMethod(() =>
                {
                    nail.transform.Find("Beam").Recycle();
                    nail.transform.Find("Glow").transform.localScale *= 2f;
                    nail.transform.Find("Glow").transform.Rotate(0f, 0f, 90f);
                    nail.transform.Find("Glow").transform.SetParent(null);
                    nail.LocateMyFSM("Control").GetState("Fire").GetAction<SetVelocityAsAngle>().speed.Value = 60f;
                    nail.LocateMyFSM("Control").FsmVariables.FindFsmFloat("Angle").Value = 0f;
                    nail.LocateMyFSM("Control").SendEvent("FINISHED");
                });
                nail.LocateMyFSM("Control").GetState("Fire").AddMethod(() =>
                {
                    StartCoroutine(DelayExecute(0.1f));
                    IEnumerator DelayExecute(float delayInSeconds)
                    {
                        yield return new WaitForSeconds(delayInSeconds);
                        nail.layer = LayerMask.NameToLayer("Attack");
                        nail.transform.localScale = new Vector3(1f, 1.8f, 1f);
                        nail.GetComponent<Rigidbody2D>().velocity = new Vector2(-160f + Math.Abs(R2), R2);
                        nail.AddComponent<NailChaser>();
                    }
                });
            }
            public void L()
            {
                Modding.Logger.Log("L");
                R2 = (float)(R1 - 0.5d) * 100f;
                var nail = GameObject.Instantiate(NAIL1, HeroController.instance.gameObject.transform.position + new Vector3(0f, -0.5f, 0), Quaternion.Euler(0f, 0f, 0f));
                nail.tag = "WaitForClean";
                Nails.Add(nail);
                nail.AddComponent<NailRecycle>();
                nail.GetComponent<DamageHero>().damageDealt = 0;
                nail.LocateMyFSM("Control").GetState("Start").AddMethod(() =>
                {
                    nail.transform.Find("Beam").Recycle();
                    nail.transform.Find("Glow").transform.localScale *= 2f;
                    nail.transform.Find("Glow").transform.Rotate(0f, 0f, 90f);
                    nail.transform.Find("Glow").transform.SetParent(null);
                    nail.LocateMyFSM("Control").GetState("Fire").GetAction<SetVelocityAsAngle>().speed.Value = 60f;
                    nail.LocateMyFSM("Control").FsmVariables.FindFsmFloat("Angle").Value = 0f;
                    nail.LocateMyFSM("Control").SendEvent("FINISHED");
                });
                nail.LocateMyFSM("Control").GetState("Fire").AddMethod(() =>
                {
                    StartCoroutine(DelayExecute(0.1f));
                    IEnumerator DelayExecute(float delayInSeconds)
                    {
                        yield return new WaitForSeconds(delayInSeconds);
                        nail.layer = LayerMask.NameToLayer("Attack");
                        nail.transform.localScale = new Vector3(1f, 1.8f, 1f);
                        nail.GetComponent<Rigidbody2D>().velocity = new Vector2(160f - Math.Abs(R2), R2);
                        nail.AddComponent<NailChaser>();
                    }
                });
            }
        }
        public class PlumeTransform1 : MonoBehaviour
        {
            public void Trans(float x)
            {
                StartCoroutine(DelayExecute(0.025f));
                IEnumerator DelayExecute(float delayInSeconds)
                {
                    yield return new WaitForSeconds(delayInSeconds);
                    gameObject.transform.position = HeroController.instance.gameObject.transform.position + new Vector3(x, -2f, 0.001f);
                }
            }
        }
        public class PlumeTransform2 : MonoBehaviour
        {
            public void Trans(float x)
            {
                StartCoroutine(DelayExecute(0.025f));
                IEnumerator DelayExecute(float delayInSeconds)
                {
                    yield return new WaitForSeconds(delayInSeconds);
                    gameObject.transform.position = HeroController.instance.gameObject.transform.position + new Vector3(x, -2f, 0.001f);
                }
            }
        }
        public class PlumeSkill2:MonoBehaviour
        {
            public float X = 0f;
            static System.Random random = new System.Random();
            static double R1 => random.NextDouble();
            static double R2 => random.NextDouble();
            void FlashScaleChange()
            {
                HeroController.instance.gameObject.transform.Find("Glow_S").gameObject.transform.localScale *= 2f;
                HeroController.instance.gameObject.transform.Find("Glow").gameObject.transform.localScale *= 2f;
            }
            void Flash_S()
            {
                FlashScaleChange();
                HeroController.instance.gameObject.transform.Find("Glow_S").gameObject.SetActive(true);
                HeroController.instance.gameObject.transform.Find("Glow").gameObject.SetActive(true);
                Invoke("FlashScaleRecover", 0.4f);
            }
            void FlashScaleRecover()
            {
                HeroController.instance.gameObject.transform.Find("Glow_S").gameObject.transform.localScale /= 2f;
                HeroController.instance.gameObject.transform.Find("Glow").gameObject.transform.localScale /= 2f;
            }
            public void Quake()
            {
                if(CanQuake && CanSuperQuake)
                {
                    Land(20, 1.6f, 80, true);
                }
                else if(CanQuake && !CanSuperQuake)
                {
                    Land(12, 1.4f, 50, false);
                }
                else
                {
                    Land(8, 1.4f, 40, false);
                }
            }
            public void Freeze()
            {
                GameManager.instance.FreezeMoment(7);
                Invoke("Flash_S", 0.15f);
            }
            public void Land(float x, float scaleFactor, int damage, bool random)
            {
                X = 2f;
                GameObject plume = GameObject.Instantiate(PLUME, HeroController.instance.gameObject.transform.position + new Vector3(30f, 30f, 0), Quaternion.Euler(0, 0, 0));
                plume.tag = "WaitForClean";
                plume.GetComponent<DamageHero>().damageDealt = 0;
                plume.LocateMyFSM("FSM").FsmVariables.FindFsmBool("Auto").Value = true;
                plume.LocateMyFSM("FSM").GetState("Antic").GetAction<Wait>().time.Value = 0f;
                plume.SetActive(true);
                plume.AddComponent<PlumeTransform2>();
                plume.GetComponent<PlumeTransform2>().Trans(0);
                HeroController.instance.gameObject.GetComponent<AudioSource>().PlayOneShot(PLUMEUP, 1f);
                SetDamageEnemy(plume, damage, 90f, 0.2f, AttackTypes.Nail);
                StartCoroutine(DelayLoopCoroutine());
                IEnumerator DelayLoopCoroutine()
                {
                    while (X <= x)
                    {
                        GameObject plume1 = GameObject.Instantiate(PLUME, HeroController.instance.gameObject.transform.position + new Vector3(30f, 30f, 0), Quaternion.Euler(0, 0, 0));
                        plume1.tag = "WaitForClean";
                        plume1.tag = "WaitForClean";
                        plume1.transform.localScale *= 0.5f * scaleFactor;
                        plume1.GetComponent<DamageHero>().damageDealt = 0;
                        plume1.LocateMyFSM("FSM").FsmVariables.FindFsmBool("Auto").Value = true;
                        plume1.LocateMyFSM("FSM").GetState("Antic").GetAction<Wait>().time.Value = 0f;
                        plume1.SetActive(true);
                        plume1.AddComponent<PlumeTransform1>();
                        plume1.GetComponent<PlumeTransform1>().Trans(X);
                        GameObject plume2 = GameObject.Instantiate(PLUME, HeroController.instance.gameObject.transform.position + new Vector3(30f, 30f, 0), Quaternion.Euler(0, 0, 0));
                        plume2.tag = "WaitForClean";
                        plume2.transform.localScale *= 0.5f * scaleFactor;
                        plume2.GetComponent<DamageHero>().damageDealt = 0;
                        plume2.LocateMyFSM("FSM").FsmVariables.FindFsmBool("Auto").Value = true;
                        plume2.LocateMyFSM("FSM").GetState("Antic").GetAction<Wait>().time.Value = 0f;
                        plume2.SetActive(true);
                        plume2.AddComponent<PlumeTransform1>();
                        plume2.GetComponent<PlumeTransform1>().Trans(-X);
                        X += 1.4f;
                        if (random)
                        {
                            plume1.transform.Rotate(0, 0, (float)(R1 - 0.5d) * 20f);
                            plume2.transform.Rotate(0, 0, (float)(R2 - 0.5d) * 20f);
                        }
                        SetDamageEnemy(plume1, damage / 2, 90f, 0.2f, AttackTypes.Nail);
                        SetDamageEnemy(plume2, damage / 2, 90f, 0.2f, AttackTypes.Nail);
                        yield return new WaitForSeconds(0.025f);
                    }
                }
            }
        }
        public class PlumeSkill1:MonoBehaviour
        {
            public float X = 0f;
            public void Land()
            {
                X = 0f;
                GameObject plume = GameObject.Instantiate(PLUME, HeroController.instance.gameObject.transform.position + new Vector3(30f, 30f, 0), Quaternion.Euler(0, 0, 0));
                plume.tag = "WaitForClean";
                plume.GetComponent<DamageHero>().damageDealt = 0;
                plume.LocateMyFSM("FSM").FsmVariables.FindFsmBool("Auto").Value = true;
                plume.LocateMyFSM("FSM").GetState("Antic").GetAction<Wait>().time.Value = 0f;
                plume.SetActive(true);
                plume.transform.localScale *= 0.7f;
                SetDamageEnemy(plume, 20, 90f, 0.2f, AttackTypes.Nail);
                StartCoroutine(DelayLoopCoroutine());
                IEnumerator DelayLoopCoroutine()
                {
                    while (X <= 8f)
                    {
                        GameObject plume1 = GameObject.Instantiate(PLUME, HeroController.instance.gameObject.transform.position + new Vector3(30f, 30f, 0), Quaternion.Euler(0, 0, 0));
                        plume1.tag = "WaitForClean";
                        plume1.GetComponent<DamageHero>().damageDealt = 0;
                        plume1.LocateMyFSM("FSM").FsmVariables.FindFsmBool("Auto").Value = true;
                        plume1.LocateMyFSM("FSM").GetState("Antic").GetAction<Wait>().time.Value = 0f;
                        plume1.SetActive(true);
                        plume1.AddComponent<PlumeTransform1>();
                        plume1.GetComponent<PlumeTransform1>().Trans(X);
                        plume1.transform.localScale *= 0.7f;
                        SetDamageEnemy(plume1, 20, 90f, 0.2f, AttackTypes.Nail);
                        GameObject plume2 = GameObject.Instantiate(PLUME, HeroController.instance.gameObject.transform.position + new Vector3(30f, 30f, 0), Quaternion.Euler(0, 0, 0));
                        plume2.tag = "WaitForClean";
                        plume2.GetComponent<DamageHero>().damageDealt = 0;
                        plume2.LocateMyFSM("FSM").FsmVariables.FindFsmBool("Auto").Value = true;
                        plume2.LocateMyFSM("FSM").GetState("Antic").GetAction<Wait>().time.Value = 0f;
                        plume2.SetActive(true);
                        plume2.AddComponent<PlumeTransform1>();
                        plume2.GetComponent<PlumeTransform1>().Trans(-X);
                        plume2.transform.localScale *= 0.7f;
                        SetDamageEnemy(plume2, 20, 90f, 0.2f, AttackTypes.Nail);
                        X += 1.5f;
                        yield return new WaitForSeconds(0.025f);
                    }
                }
            }
        }
        public class FocusUp : MonoBehaviour
        {
            static System.Random random = new System.Random();
            static double R1 => random.NextDouble();
            static double R2 => random.NextDouble();
            GameObject FOCUSBLAST;
            float speed_orig;
            public void Focus(float r, float speed)
            {
                foreach(Transform child in HeroController.instance.gameObject.transform)
                {
                    if (child.gameObject.name.Contains("Blast"))
                        child.Recycle();
                }
                var blast = Instantiate(BIGBLAST, HeroController.instance.gameObject.transform.position + new Vector3(0f,2f,0f), new Quaternion(), HeroController.instance.gameObject.transform);
                blast.tag = "WaitForClean";
                blast.transform.localScale *= r;
                speed_orig = blast.GetComponent<Animator>().speed;
                blast.GetComponent<Animator>().speed = speed;
                blast.SetActive(true);
                blast.AddComponent<PtRecycle>();
                FOCUSBLAST = blast;
                var pt1 = FOCUSBLAST.transform.Find("Particle_rocks_small").gameObject;
                pt1.Recycle();
            }
            public void FocusLittle(bool deep)
            {
                int count = 0;
                int countMax = 5;
                int damage = 21;
                float r = 0.7f;
                if(deep)
                {
                    r = 1f;
                    countMax = 5;
                    damage = 48;
                }
                else
                {
                    r = 0.7f;
                    countMax = 4;
                    damage = 32;
                }
                float sigh = Math.Sign(R1 - 0.5d);
                float sigh2 = 1;
                StartCoroutine(DelayLoopCoroutine());
                IEnumerator DelayLoopCoroutine()
                {
                    while (count < countMax)
                    {
                        float x;
                        count++;
                        if(deep)
                        {
                            x = sigh * ((2.5f - count) / 2.5f) * 18 + (float)R1 * 1.5f;
                        }
                        else
                        {
                            x = sigh * ((2f - count) / 2f) * 18 + (float)R1 * 1.5f;
                        }
                        var blast = Instantiate(BLAST, HeroController.instance.gameObject.transform.position + new Vector3(x, (float)R2 * 3.5f * sigh2 + 1, 0f), Quaternion.Euler(0f, 0f, 0f));
                        blast.tag = "WaitForClean";
                        blast.AddComponent<SoonRecycle>();
                        var blastchild = blast.transform.Find("Blast").gameObject;
                        blastchild.transform.localScale = new Vector3(r, r, 1f);
                        blastchild.transform.Find("hero_damager").GetComponent<DamageHero>().damageDealt = 0;
                        blastchild.transform.Find("hero_damager").gameObject.layer = LayerMask.NameToLayer("Attack");
                        SetDamageEnemy(blastchild.transform.Find("hero_damager").gameObject, damage, 0f, 0f, AttackTypes.Spell);
                        blastchild.SetActive(true);
                        sigh2 = -sigh2;
                        yield return new WaitForSeconds(0.1f);
                    }
                }
            }

            public void FocusBlast(float r, int damage)
            {
                FOCUSBLAST.GetComponent<Animator>().speed = speed_orig;
                var hit = Instantiate(BIGBLASTHIT, HeroController.instance.gameObject.transform.position + new Vector3(0f, 2f, 0f), Quaternion.Euler(0, 0, 0));
                hit.tag = "WaitForClean";
                hit.transform.localScale *= r;
                hit.GetComponent<DamageHero>().damageDealt = 0;
                hit.layer = LayerMask.NameToLayer("Attack");
                hit.SetActive(true);
                SetDamageEnemy(hit, damage, 90f + HeroController.instance.gameObject.transform.localScale.x * 90f, r * 2f, AttackTypes.Spell);
                StartCoroutine(DelayExecute(0.1f));
                IEnumerator DelayExecute(float delayInSeconds)
                {
                    yield return new WaitForSeconds(delayInSeconds);
                    hit.Recycle();
                }
                var pt2 = FOCUSBLAST.transform.Find("Particle edge_build").gameObject;
                var pt3 = FOCUSBLAST.transform.Find("Particle middle").gameObject;
                pt2.transform.SetParent(null);
                pt3.transform.SetParent(null);
                pt2.AddComponent<PtRecycle>();
                pt3.AddComponent<PtRecycle>();
                if (HeroController.instance.playerData.GetBool("equippedCharm_35") && GameManager.instance.GetPlayerDataInt("health") < GameManager.instance.playerData.CurrentMaxHealth)
                {

                    if (HeroController.instance.playerData.GetBool("equippedCharm_34"))
                    {
                        FocusLittle(true);
                    }
                    else
                    {
                        FocusLittle(false);
                    }
                }
            }
            public void FocusCancel()
            {
                if(FOCUSBLAST != null)
                {
                    FOCUSBLAST.Recycle();
                }
            }
        }
        public class NewGreatSlash : MonoBehaviour
        {
            public void DoSlash()
            {
                Invoke("ChangeScale", 0f);
                Invoke("OneSlash", 0f);
                Invoke("AngleReverse", 0.18f);
                Invoke("OneSlash", 0.2f);
                Invoke("AngleReverse", 0.38f);
                Invoke("OneSlash", 0.4f);
                Invoke("RecoverScale", 0.42f);
            }
            public void OneSlash()
            {
                HeroController.instance.Attack(AttackDirection.normal);
            }
            public void ChangeScale()
            {
                NailType = 1;
            }
            public void RecoverScale()
            {
                NailType = 0;
            }
            public void AngleReverse()
            {
                SlashAngleReverse = !SlashAngleReverse;
            }
        }
        public class NewDashSlash : MonoBehaviour
        {
            static System.Random random = new System.Random();
            static double R1 => random.NextDouble();
            public int DashCount = 0;
            bool ContinueDashTimer_On = false;
            public bool LongDistance = false;
            public bool LongLongDistance = false;
            static float dashCheckTime = 0.2f;
            float S_StartTimer = 0f;
            GameObject shadow;
            public void Awake()
            {
                var nail1 = GameObject.Instantiate(NAIL1, HeroController.instance.gameObject.transform.position + new Vector3(0f, -0.5f, 0), Quaternion.Euler(0f, 0f, 0f));
                nail1.tag = "WaitForClean";
                nail1.transform.Find("Beam").Recycle();
                var Glow_small = nail1.transform.Find("Glow").gameObject;
                nail1.transform.Find("Glow").transform.localScale = new Vector3(1.4f, 1.3f, 1f) / 2f;
                Glow_small.name = "Glow_small";
                Glow_small.transform.SetParent(HeroController.instance.gameObject.transform);
                nail1.transform.position = HeroController.instance.gameObject.transform.position + new Vector3(0f, 100f, 0);
                nail1.SetActive(false);
                if(HeroController.instance.gameObject.transform.Find("Attacks").gameObject != null)
                {
                    var attacks = HeroController.instance.gameObject.transform.Find("Attacks").gameObject;
                    shadow = attacks.transform.Find("Sharp Shadow").gameObject;
                    shadow.transform.localScale *= 2f;
                }
                else
                {
                    Invoke("Repeat", 0.2f);
                }
            }
            public void Update()
            {
                if (ContinueDashTimer_On && GameManager.instance.inputHandler.inputActions.dash.IsPressed && GameManager.instance.inputHandler.inputActions.dash.HasChanged)
                {
                    HeroController.instance.Reflect().FinishedDashing();
                    if(S_StartTimer > 0)
                    {
                        HeroController.instance.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                        CancelInvoke("Return");
                        Invoke("Return", 0.5f);
                        DashSpeedAdd();
                        Audio2();
                        Flash_M();
                        DoDash_S();
                        ContinueDashTimer_End();
                        CancelInvoke("ContinueDashTimer_End");
                        Invoke("DashSpeedReset", 0.35f);
                    }
                    else
                    {
                        DashSpeedAdd();
                        Audio2();
                        Flash_M();
                        DoDash();
                        ContinueDashTimer_End();
                        CancelInvoke("ContinueDashTimer_End");
                        Invoke("DashSpeedReset", 0.35f);
                    }
                }
                if(S_StartTimer > 0)
                {
                    S_StartTimer -= Time.deltaTime;
                }
            }
            public void Repeat()
            {
                var attacks = HeroController.instance.gameObject.transform.Find("Attacks").gameObject;
                shadow = attacks.transform.Find("Sharp Shadow").gameObject;
                shadow.transform.localScale *= 2f;
            }

            void DamageChange_Slash()
            {
                var attack = HeroController.instance.gameObject.transform.Find("Attacks").gameObject;
                var slash = attack.transform.Find("Slash").gameObject;
                var altslash = attack.transform.Find("AltSlash").gameObject;
                if (HeroController.instance.playerData.GetBool("equippedCharm_25"))
                {
                    slash.LocateMyFSM("damages_enemy").FsmVariables.FindFsmInt("damageDealt").Value = (int)(PlayerData.instance.nailDamage * 2.25f);
                    altslash.LocateMyFSM("damages_enemy").FsmVariables.FindFsmInt("damageDealt").Value = (int)(PlayerData.instance.nailDamage * 2.25f);
                }
                else
                {
                    slash.LocateMyFSM("damages_enemy").FsmVariables.FindFsmInt("damageDealt").Value = (int)(PlayerData.instance.nailDamage * 1.5f);
                    altslash.LocateMyFSM("damages_enemy").FsmVariables.FindFsmInt("damageDealt").Value = (int)(PlayerData.instance.nailDamage * 1.5f);
                }
                CancelInvoke("DamageRecover_Slash");
                Invoke("DamageRecover_Slash", 0.3f);
            }
            void DamageRecover_Slash()
            {
                HeroController.instance.gameObject.transform.Find("Attacks").gameObject.LocateMyFSM("Set Slash Damage").SetState("Init");
            }
            void DamageChange()
            {
                var attack = HeroController.instance.gameObject.transform.Find("Attacks").gameObject;
                var slash = attack.transform.Find("Sharp Shadow").gameObject;
                if (!HeroController.instance.playerData.GetBool("equippedCharm_35"))
                {
                    if(HeroController.instance.playerData.GetBool("equippedCharm_6"))
                    {
                        shadow.LocateMyFSM("damages_enemy").FsmVariables.FindFsmInt("damageDealt").Value = (int)(PlayerData.instance.nailDamage * 4.375f);
                    }
                    else
                    {
                        shadow.LocateMyFSM("damages_enemy").FsmVariables.FindFsmInt("damageDealt").Value = (int)(PlayerData.instance.nailDamage * 2.5f);
                    }
                }
                else
                {
                    if (HeroController.instance.playerData.GetBool("equippedCharm_6"))
                    {
                        shadow.LocateMyFSM("damages_enemy").FsmVariables.FindFsmInt("damageDealt").Value = (int)(PlayerData.instance.nailDamage * 3.5f);
                    }
                    else
                    {
                        shadow.LocateMyFSM("damages_enemy").FsmVariables.FindFsmInt("damageDealt").Value = (int)(PlayerData.instance.nailDamage * 2f);
                    }
                }
            }
            void DamageChange_S()
            {
                var attack = HeroController.instance.gameObject.transform.Find("Attacks").gameObject;
                var slash = attack.transform.Find("Sharp Shadow").gameObject;
                if (!HeroController.instance.playerData.GetBool("equippedCharm_35"))
                {
                    if (HeroController.instance.playerData.GetBool("equippedCharm_6"))
                    {
                        shadow.LocateMyFSM("damages_enemy").FsmVariables.FindFsmInt("damageDealt").Value = (int)(PlayerData.instance.nailDamage * 7f);
                    }
                    else
                    {
                        shadow.LocateMyFSM("damages_enemy").FsmVariables.FindFsmInt("damageDealt").Value = (int)(PlayerData.instance.nailDamage * 4f);
                    }
                }
                else
                {
                    if (HeroController.instance.playerData.GetBool("equippedCharm_6"))
                    {
                        shadow.LocateMyFSM("damages_enemy").FsmVariables.FindFsmInt("damageDealt").Value = (int)(PlayerData.instance.nailDamage * 5.25f);
                    }
                    else
                    {
                        shadow.LocateMyFSM("damages_enemy").FsmVariables.FindFsmInt("damageDealt").Value = (int)(PlayerData.instance.nailDamage * 3f);
                    }
                }
            }
            void DamageRecover()
            {
                shadow.LocateMyFSM("damages_enemy").FsmVariables.FindFsmInt("damageDealt").Value = PlayerData.instance.nailDamage;
            }
            void DashSpeedReset()
            {
                if (LongDistance == true)
                {
                    LongDistance = false;
                }
            }
            void DashSpeedAdd()
            {
                LongDistance = true;
            }
            public void Reset()
            {
                ContinueDashTimer_On = false;
            }
            public void Flash()
            {
                if (HeroController.instance.gameObject.transform.Find("Glow_small").gameObject != null)
                {
                    HeroController.instance.gameObject.transform.Find("Glow_small").gameObject.SetActive(true);
                }
            }
            void ResetScale()
            {
                HeroController.instance.gameObject.transform.Find("Glow").localEulerAngles = new Vector3(0f, 0f, 0f);
                HeroController.instance.gameObject.transform.Find("Glow_small").localEulerAngles = new Vector3(0f, 0f, 0f);
            }
            public void Flash_M()
            {
                if (HeroController.instance.gameObject.transform.Find("Glow").gameObject != null)
                {
                    HeroController.instance.gameObject.transform.Find("Glow").gameObject.SetActive(true);
                }
            }
            void Freeze()
            {
                GameManager.instance.FreezeMoment(3);
            }
            public void Return()
            {
                Flash_M();
                Audio3();
                HeroController.instance.gameObject.transform.position = PositionBeforeDashS + new Vector3(0f,0.1f,0f);
                HeroController.instance.gameObject.GetComponent<BoxCollider2D>().enabled = true;
                HeroController.instance.parryInvulnTimer = 0.5f;
                if(LastOnGround)
                {
                    HeroController.instance.gameObject.LocateMyFSM("Spell Control").SetState("Quake1 Down");
                }
            }
            public void FirstContinueDashTimer_Start_S()
            {
                PositionBeforeDashS = HeroController.instance.gameObject.transform.position;
                if (HeroController.instance.cState.onGround)
                {
                    LastOnGround = true;
                }
                else
                {
                    LastOnGround = false;
                }
                DashCount = 1; 
                Invoke("ContinueDashTimer_Start_S", 0.1f);
                HeroController.instance.parryInvulnTimer = 0.6f;
                Audio3();
            }
            public void FirstContinueDashTimer_Start()
            {
                dashCheckTime = 0.2f;
                DashCount = 1;
                Invoke("ContinueDashTimer_Start", 0.1f);
                HeroController.instance.parryInvulnTimer = 0.6f;
            }
            public void Dashing_S_End()
            {
                Dashing_S = false;
            }
            public void ContinueDashTimer_Start_S()
            {
                ContinueDashTimer_On = true;
                S_StartTimer = 0.5f;
                Dashing_S = true;
                CancelInvoke("Dashing_S_End");
                Invoke("Dashing_S_End", 0.35f);
                HeroController.instance.parryInvulnTimer = 0.6f;
                Invoke("ContinueDashTimer_End", dashCheckTime);
                Invoke("Audio1", 0.1f);
                Flash();
            }
            public void ContinueDashTimer_Start()
            {
                ContinueDashTimer_On = true;
                HeroController.instance.parryInvulnTimer = 0.6f;
                Invoke("ContinueDashTimer_End", dashCheckTime);
                Invoke("Audio1", 0.1f);
                Flash();
            }
            public void Audio1()
            {
                HeroController.instance.gameObject.GetComponent<AudioSource>().PlayOneShot(BLOCKING, 1.5f);
            }
            public void Audio2()
            {
                HeroController.instance.gameObject.GetComponent<AudioSource>().PlayOneShot(BLOCKSUCCEED, 1.5f);
            }
            public void Audio3()
            {
                HeroController.instance.gameObject.GetComponent<AudioSource>().PlayOneShot(TELE, 1f);
            }
            void ContinueDashTimer_End()
            {
                ContinueDashTimer_On = false;
            }
            void CancelDashSlash()
            {
                HeroController.instance.gameObject.LocateMyFSM("Nail Arts").SendEvent("DASH CHECK");
            }
            void DashSlashAnim()
            {
                HeroController.instance.gameObject.LocateMyFSM("Nail Arts").SetState("Can Nail Art?");
            }
            public void DoDash_S()
            {
                DashDashing = true;
                DamageChange_S();
                CancelInvoke("DamageRecover");
                Invoke("DamageRecover", 0.4f);
                HeroController.instance.Reflect().HeroDash();
                HeroController.instance.parryInvulnTimer = 0.5f;
                if (HeroController.instance.playerData.GetBool("equippedCharm_31"))
                {
                    if (DashCount <= 6)
                    {
                        DashCount++;
                        Invoke("ContinueDashTimer_Start_S", 0.3f);
                    }
                }
                else
                {
                    if (DashCount <= 4)
                    {
                        DashCount++;
                        Invoke("ContinueDashTimer_Start_S", 0.3f);
                    }
                }
            }
            public void DoDash()
            {
                DashDashing = true;
                //CancelDashSlash();
                CancelInvoke("DamageRecover");
                DamageChange();
                Invoke("DamageRecover", 0.4f);
                HeroController.instance.Reflect().HeroDash();
                HeroController.instance.parryInvulnTimer = 0.4f;
                if (HeroController.instance.playerData.GetBool("equippedCharm_31"))
                {
                    if (DashCount <= 4)
                    {
                        DashCount++;
                        Invoke("ContinueDashTimer_Start", 0.3f);
                        Invoke("DamageChange_Slash", 0.3f);
                    }
                }
                else
                {
                    if (DashCount <= 2)
                    {
                        DashCount++;
                        Invoke("ContinueDashTimer_Start", 0.3f);
                        Invoke("DamageChange_Slash", 0.3f);
                    }
                }
            }
        }
        public class NewCycloneSlash : MonoBehaviour
        {
            GameObject tendrils_L = null;
            int slashCount = 1;
            public void Awake()
            {
                if(tendrils_L == null)
                {
                    tendrils_L = Instantiate(TENTRILS, HeroController.instance.gameObject.transform.position + new Vector3(1f, 0f, 0f), new Quaternion(), HeroController.instance.gameObject.transform);
                    tendrils_L.transform.Find("T Hit").gameObject.GetComponent<DamageHero>().damageDealt = 0;
                    Vector2[] vectorArray = { new Vector2(3f, 2.4f), new Vector2(-1f, 0.8f), new Vector2(3f, -1.2f), new Vector2(16f, -2.5f), new Vector2(16f, 2.7f) };

                    tendrils_L.transform.Find("T Hit").gameObject.GetComponent<PolygonCollider2D>().points = vectorArray;
                    tendrils_L.transform.Find("T Hit").gameObject.AddComponent<CyclonePlumeSummon>();
                    tendrils_L.transform.Find("T Hit").gameObject.layer = LayerMask.NameToLayer("Attack");
                    SetDamageEnemy(tendrils_L.transform.Find("T Hit").gameObject, 30, 90f, 0f, AttackTypes.SharpShadow);
                    tendrils_L.transform.localScale = new Vector3(-0.3f, 0.8f, 1f);
                    tendrils_L.transform.localPosition = new Vector3(-1.2f, 0f, 0f);
                    tendrils_L.transform.localEulerAngles = new Vector3(0f, 0f, -6f);
                }
            }
            public void Slash()
            {
                GameObject closest = null;
                GameObject hero = HeroController.instance.gameObject;
                float closestDistance = Mathf.Infinity;
                float angle;


                foreach (GameObject obj in GameObject.FindObjectsOfType<GameObject>())
                {
                    if (obj != tendrils_L.transform.Find("T Hit").gameObject && obj != hero && obj.GetComponent<HealthManager>() != null && !obj.name.Contains("Zote Balloon"))
                    {
                        float distance = Vector2.Distance(HeroController.instance.gameObject.transform.position, obj.transform.position);

                        if (distance < closestDistance)
                        {
                            closest = obj;
                            closestDistance = distance;
                        }
                    }
                }
                if (closest != null)
                {
                    if(hero.transform.localScale.x < 0)
                    {
                        angle = (float)Math.Atan((hero.transform.position.y - closest.transform.position.y) / (hero.transform.position.x - closest.transform.position.x));
                        if (closest.transform.position.x - hero.transform.position.x < 0)
                        {
                            angle += (float)Math.PI;
                        }
                        tendrils_L.transform.localPosition = new Vector3(0, 0, 0f);
                        tendrils_L.transform.localEulerAngles = new Vector3(0f, 0f, -(float)RadiansToDegrees(angle) - 6f);
                    }
                    else
                    {
                        angle = (float)Math.Atan((hero.transform.position.y - closest.transform.position.y) / (hero.transform.position.x - closest.transform.position.x));
                        if (closest.transform.position.x - hero.transform.position.x < 0)
                        {
                            angle += (float)Math.PI;
                        }
                        tendrils_L.transform.localPosition = new Vector3(0, 0, 0f);
                        tendrils_L.transform.localEulerAngles = new Vector3(0f, 0f, (float)RadiansToDegrees(angle) + 174f);
                    }
                }
                else
                {
                    tendrils_L.transform.localPosition = new Vector3(-1.2f, 0f, 0f);
                    tendrils_L.transform.localEulerAngles = new Vector3(0f, 0f, -6f);
                }

                int damage = HeroController.instance.playerData.nailDamage;
                if (HeroController.instance.playerData.GetBool("equippedCharm_16"))
                {
                    tendrils_L.transform.localScale = new Vector3(-(0.3f + NailChargeCount * 0.1f), 0.5f + NailChargeCount * 0.1f, 1f);
                    if (!HeroController.instance.playerData.GetBool("equippedCharm_35"))
                    {
                        damage = (int)(damage * 1.5f);
                    }
                }
                else
                {
                    tendrils_L.transform.localScale = new Vector3(-(0.3f + NailChargeCount * 0.1f), 0.5f + NailChargeCount * 0.1f, 1f);
                    if (HeroController.instance.playerData.GetBool("equippedCharm_35"))
                    {
                        damage = (int)(damage * 0.75f);
                    }
                }
                SetDamageEnemy(tendrils_L.transform.Find("T Hit").gameObject, damage, 90 + HeroController.instance.gameObject.transform.localScale.x * 90, 0.5f, AttackTypes.SharpShadow);
                if(slashCount == 1)
                {
                    tendrils_L.transform.Find("T1").gameObject.SetActive(true);
                }
                if(slashCount == 2)
                {
                    tendrils_L.transform.Find("T2").gameObject.SetActive(true);
                }
                if(slashCount == 3)
                {
                    tendrils_L.transform.Find("T3").gameObject.SetActive(true);
                }
                if(slashCount >= 3)
                {
                    slashCount = 1;
                }
                slashCount++;
                tendrils_L.transform.Find("T Hit").gameObject.SetActive(true);
                CancelInvoke("SlashEnd");
                Invoke("SlashEnd", 0.5f);
            }
            public void Slash_S()
            {
                GameObject closest = null;
                GameObject hero = HeroController.instance.gameObject;
                float closestDistance = Mathf.Infinity;
                float angle;


                foreach (GameObject obj in GameObject.FindObjectsOfType<GameObject>())
                {
                    if (obj != tendrils_L.transform.Find("T Hit").gameObject && obj != hero && obj.GetComponent<HealthManager>() != null && !obj.name.Contains("Zote Balloon"))
                    {
                        float distance = Vector2.Distance(HeroController.instance.gameObject.transform.position, obj.transform.position);

                        if (distance < closestDistance)
                        {
                            closest = obj;
                            closestDistance = distance;
                        }
                    }
                }
                if (closest != null)
                {
                    if(hero.transform.localScale.x < 0)
                    {
                        angle = (float)Math.Atan((hero.transform.position.y - closest.transform.position.y) / (hero.transform.position.x - closest.transform.position.x));
                        if (closest.transform.position.x - hero.transform.position.x < 0)
                        {
                            angle += (float)Math.PI;
                        }
                        tendrils_L.transform.localPosition = new Vector3(0, 0, 0f);
                        tendrils_L.transform.localEulerAngles = new Vector3(0f, 0f, -(float)RadiansToDegrees(angle) - 6f);
                    }
                    else
                    {
                        angle = (float)Math.Atan((hero.transform.position.y - closest.transform.position.y) / (hero.transform.position.x - closest.transform.position.x));
                        if (closest.transform.position.x - hero.transform.position.x < 0)
                        {
                            angle += (float)Math.PI;
                        }
                        tendrils_L.transform.localPosition = new Vector3(0, 0, 0f);
                        tendrils_L.transform.localEulerAngles = new Vector3(0f, 0f, (float)RadiansToDegrees(angle) + 174f);
                    }
                }
                else
                {
                    tendrils_L.transform.localPosition = new Vector3(-1.2f, 0f, 0f);
                    tendrils_L.transform.localEulerAngles = new Vector3(0f, 0f, -6f);
                }

                int damage = HeroController.instance.playerData.nailDamage * 3;
                if (HeroController.instance.playerData.GetBool("equippedCharm_16"))
                {
                    tendrils_L.transform.localScale = new Vector3(-0.9f, 0.9f, 1f);
                    if (!HeroController.instance.playerData.GetBool("equippedCharm_35"))
                    {
                        damage = (int)(damage * 1.5f);
                    }
                }
                else
                {
                    if (HeroController.instance.playerData.GetBool("equippedCharm_35"))
                    {
                        damage = (int)(damage * 0.75f);
                    }
                }
                SetDamageEnemy(tendrils_L.transform.Find("T Hit").gameObject, damage, 90 + HeroController.instance.gameObject.transform.localScale.x * 90, 0.5f, AttackTypes.SharpShadow);

                tendrils_L.transform.Find("T1").gameObject.SetActive(false);

                tendrils_L.transform.Find("T2").gameObject.SetActive(false);

                tendrils_L.transform.Find("T3").gameObject.SetActive(false);

                tendrils_L.transform.Find("T1").gameObject.SetActive(true);

                tendrils_L.transform.Find("T2").gameObject.SetActive(true);

                tendrils_L.transform.Find("T3").gameObject.SetActive(true);

                tendrils_L.transform.Find("T Hit").gameObject.SetActive(true);
                CancelInvoke("SlashEnd_S");
                Invoke("SlashEnd_S", 0.5f);
            }
            void SlashEnd()
            {
                tendrils_L.transform.Find("T Hit").gameObject.SetActive(false);
                if(NailChargeCount >= 1)
                {
                    NailChargeCount--;
                }
            }
            void SlashEnd_S()
            {
                tendrils_L.transform.Find("T Hit").gameObject.SetActive(false);
                SuperNailCharged = false;
                PT.GetComponent<ParticleSystem>().enableEmission = false;
            }
        }
        public class WaveSpeedUp : MonoBehaviour
        {
            void Update()
            {
                gameObject.GetComponent<Rigidbody2D>().velocity += new Vector2(Math.Sign(gameObject.GetComponent<Rigidbody2D>().velocity.x) * 50f * Time.deltaTime, 0f);
            }
        }
        public class HeroBlocking : MonoBehaviour
        {
            static System.Random random = new System.Random();
            static double R1 => random.NextDouble();
            bool Slashed = true;
            public void Awake()
            {
                var nail = GameObject.Instantiate(NAIL1, HeroController.instance.gameObject.transform.position + new Vector3(0f, -0.5f, 0), Quaternion.Euler(0f, 0f, 0f));
                nail.tag = "WaitForClean";
                nail.transform.Find("Beam").Recycle();
                nail.transform.Find("Glow").transform.localScale = new Vector3(1.4f, 1.3f, 0.5f);
                nail.transform.Find("Glow").transform.Rotate(0f, 0f, 90f);
                nail.transform.Find("Glow").transform.SetParent(HeroController.instance.gameObject.transform);
                nail.transform.position = HeroController.instance.gameObject.transform.position + new Vector3(0f, 100f, 0);
                nail.SetActive(false);
                var nail1 = GameObject.Instantiate(NAIL1, HeroController.instance.gameObject.transform.position + new Vector3(0f, -0.5f, 0), Quaternion.Euler(0f, 0f, 0f));
                nail.tag = "WaitForClean";
                nail1.transform.Find("Beam").Recycle();
                nail1.transform.Find("Glow").transform.localScale = new Vector3(1.4f, 1.3f, 0.5f);
                var Glow_S = nail1.transform.Find("Glow").gameObject;
                Glow_S.name = "Glow_S";
                Glow_S.transform.SetParent(HeroController.instance.gameObject.transform);
                nail1.transform.position = HeroController.instance.gameObject.transform.position + new Vector3(0f, 100f, 0);
                nail1.SetActive(false);
            }
            public void BlockFlash()
            {
                if(HeroController.instance.gameObject.transform.Find("Glow").gameObject != null)
                {
                    HeroController.instance.gameObject.transform.Find("Glow").gameObject.SetActive(true);
                }
            }
            public void BlockFlashCancel()
            {
                if (HeroController.instance.gameObject.transform.Find("Glow").gameObject != null)
                {
                    HeroController.instance.gameObject.transform.Find("Glow_S").gameObject.SetActive(false);
                    HeroController.instance.gameObject.transform.Find("Glow").gameObject.SetActive(false);
                }
            }
            public void BlockFlash_S_Try()
            {
                if (HeroController.instance.gameObject.transform.Find("Glow_S").gameObject != null && HeroController.instance.gameObject.transform.Find("Glow").gameObject != null)
                {
                    FlashScaleChange();
                    HeroController.instance.gameObject.transform.Find("Glow_S").gameObject.SetActive(true);
                    HeroController.instance.gameObject.transform.Find("Glow").gameObject.SetActive(true);
                    Invoke("FlashScaleRecover", 0.4f);
                }
            }
            public void BlockFlash_S()
            {
                if (HeroController.instance.gameObject.transform.Find("Glow_S").gameObject != null && HeroController.instance.gameObject.transform.Find("Glow").gameObject != null)
                {
                    CancelInvoke("FlashScaleRecover");
                    HeroController.instance.gameObject.transform.Find("Glow_S").gameObject.SetActive(true);
                    HeroController.instance.gameObject.transform.Find("Glow").gameObject.SetActive(true);
                    Invoke("FlashScaleRecover", 0.4f);
                }
            }
            void FlashScaleChange()
            {
                HeroController.instance.gameObject.transform.Find("Glow_S").gameObject.transform.localScale *= 2f;
                HeroController.instance.gameObject.transform.Find("Glow").gameObject.transform.localScale *= 2f;
            }
            void FlashScaleRecover()
            {
                HeroController.instance.gameObject.transform.Find("Glow_S").gameObject.transform.localScale /= 2f;
                HeroController.instance.gameObject.transform.Find("Glow").gameObject.transform.localScale /= 2f;
            }
            public void BlockShortTime()
            {
                Slashed = false;
                Blocking = true;
                Invoke("BlockEnd", 0.1f);
                HeroController.instance.EndTakeNoDamage();
            }
            public void BlockLongTime()
            {
                Slashed = false;
                Blocking = true;
                Invoke("BlockEnd", 0.165f);
                HeroController.instance.EndTakeNoDamage();
            }
            public void BlockReapetTime()
            {
                Blocking = true;
                Invoke("BlockEnd", 0.23f);
                HeroController.instance.EndTakeNoDamage();
            }
            void BlockEnd()
            {
                Blocking = false;
                if (Slashed == false)
                {
                    Revenge_W();
                    Slashed = true;
                }
            }
            void CanSlashEnd()
            {
                CanSlash = false;
            }
            void OneSlash()
            {
                CancelInvoke("CanSlashEnd");
                CanSlash = true;
                HeroController.instance.Attack(AttackDirection.normal);
                Invoke("CanSlashEnd", 0.23f);
            }
            void ChangeScale_M()
            {
                NailType = 3;
            }
            void ChangeScale_S_M()
            {
                NailType = 4;
            }
            void ChangeScale()
            {
                if (HeroController.instance.playerData.GetBool("equippedCharm_13"))
                    NailType = 3;
                else
                    NailType = 5;
            }
            void ChangeScale_S()
            {
                if (HeroController.instance.playerData.GetBool("equippedCharm_13"))
                    NailType = 4;
                else
                    NailType = 6;
            }
            void RecoverScale()
            {
                NailType = 0;
            }
            void Freeze()
            {
                GameManager.instance.FreezeMoment(3);
            }
            void Freeze_S()
            {
                GameManager.instance.FreezeMoment(6);
            }
            void Invuln()
            {
                HeroController.instance.parryInvulnTimer = 0.6f;
            }
            void AnimRecover()
            {
                HeroController.instance.Reflect().CancelAttack();
            }
            void DamageChange_W()
            {
                var attack = HeroController.instance.gameObject.transform.Find("Attacks").gameObject;
                var slash = attack.transform.Find("Slash").gameObject;
                var altslash = attack.transform.Find("AltSlash").gameObject;
                if(HeroController.instance.playerData.GetBool("equippedCharm_16"))
                {
                    slash.LocateMyFSM("damages_enemy").FsmVariables.FindFsmInt("damageDealt").Value = (int)(PlayerData.instance.nailDamage * 1.5f);
                    altslash.LocateMyFSM("damages_enemy").FsmVariables.FindFsmInt("damageDealt").Value = (int)(PlayerData.instance.nailDamage * 1.5f);
                }
                else
                {
                    slash.LocateMyFSM("damages_enemy").FsmVariables.FindFsmInt("damageDealt").Value = (int)(PlayerData.instance.nailDamage);
                    altslash.LocateMyFSM("damages_enemy").FsmVariables.FindFsmInt("damageDealt").Value = (int)(PlayerData.instance.nailDamage);
                }
            }
            void DamageChange()
            {
                var attack = HeroController.instance.gameObject.transform.Find("Attacks").gameObject;
                var slash = attack.transform.Find("Slash").gameObject;
                var altslash = attack.transform.Find("AltSlash").gameObject;
                if (HeroController.instance.playerData.GetBool("equippedCharm_35"))
                {
                    slash.LocateMyFSM("damages_enemy").FsmVariables.FindFsmInt("damageDealt").Value = PlayerData.instance.nailDamage * 4 + 6;
                    altslash.LocateMyFSM("damages_enemy").FsmVariables.FindFsmInt("damageDealt").Value = PlayerData.instance.nailDamage * 4 + 6;
                }
                else
                {
                    slash.LocateMyFSM("damages_enemy").FsmVariables.FindFsmInt("damageDealt").Value = PlayerData.instance.nailDamage * 6 - 6;
                    altslash.LocateMyFSM("damages_enemy").FsmVariables.FindFsmInt("damageDealt").Value = PlayerData.instance.nailDamage * 6 - 6;
                }
            }
            void DamageChange_S()
            {
                var attack = HeroController.instance.gameObject.transform.Find("Attacks").gameObject;
                var slash = attack.transform.Find("Slash").gameObject;
                var altslash = attack.transform.Find("AltSlash").gameObject;
                if (HeroController.instance.playerData.GetBool("equippedCharm_15"))
                {
                    if(HeroController.instance.playerData.GetInt("health") == 1 && HeroController.instance.playerData.GetBool("equippedCharm_6"))
                    {
                        slash.LocateMyFSM("damages_enemy").FsmVariables.FindFsmInt("damageDealt").Value = PlayerData.instance.nailDamage * 11;
                        altslash.LocateMyFSM("damages_enemy").FsmVariables.FindFsmInt("damageDealt").Value = PlayerData.instance.nailDamage * 11;
                    }
                    else
                    {
                        slash.LocateMyFSM("damages_enemy").FsmVariables.FindFsmInt("damageDealt").Value = (int)((float)(PlayerData.instance.nailDamage * 11) * 1.75f);
                        altslash.LocateMyFSM("damages_enemy").FsmVariables.FindFsmInt("damageDealt").Value = (int)((float)(PlayerData.instance.nailDamage * 11) * 1.75f);
                    }
                }
                else
                {
                    if (HeroController.instance.playerData.GetInt("health") == 1 && HeroController.instance.playerData.GetBool("equippedCharm_6"))
                    {
                        slash.LocateMyFSM("damages_enemy").FsmVariables.FindFsmInt("damageDealt").Value = PlayerData.instance.nailDamage * 8;
                        altslash.LocateMyFSM("damages_enemy").FsmVariables.FindFsmInt("damageDealt").Value = PlayerData.instance.nailDamage * 8;
                    }
                    else
                    {
                        slash.LocateMyFSM("damages_enemy").FsmVariables.FindFsmInt("damageDealt").Value = (int)((float)(PlayerData.instance.nailDamage * 8) * 1.75f);
                        altslash.LocateMyFSM("damages_enemy").FsmVariables.FindFsmInt("damageDealt").Value = (int)((float)(PlayerData.instance.nailDamage * 8) * 1.75f);
                    }
                }
            }
            void SummonNail()
            {
                float R = (float)R1 - 0.5f;
                var nail = GameObject.Instantiate(NAIL1, HeroController.instance.gameObject.transform.position + new Vector3(R * 20f, 14f, 0), Quaternion.Euler(0f, 0f, 180f));
                nail.tag = "WaitForClean";
                Nails.Add(nail);
                nail.AddComponent<NailRecycle>();
                nail.GetComponent<DamageHero>().damageDealt = 0;
                nail.LocateMyFSM("Control").GetState("Start").AddMethod(() =>
                {
                    nail.transform.Find("Beam").Recycle();
                    nail.transform.Find("Glow").transform.localScale *= 2f;
                    nail.transform.Find("Glow").transform.SetParent(null);
                    nail.LocateMyFSM("Control").FsmVariables.FindFsmFloat("Angle").Value = 0f;
                    nail.LocateMyFSM("Control").SendEvent("FINISHED");
                });
                nail.LocateMyFSM("Control").GetState("Fire").AddMethod(() =>
                {
                    nail.layer = LayerMask.NameToLayer("Attack");
                    nail.transform.localScale = new Vector3(1f, 1.8f, 1f);
                    nail.AddComponent<BlockingNailFly>();
                });
            }
            void DamageRecover()
            {
                HeroController.instance.gameObject.transform.Find("Attacks").gameObject.LocateMyFSM("Set Slash Damage").SetState("Init");
            }
            public void S_Ready_ConsumeLately()
            {
                Invoke("S_Ready_Consume", 0.4f);
            }
            void S_Ready_Consume()
            {
                SuperNailCharged = false;
                PT.GetComponent<ParticleSystem>().enableEmission = false;
            }
            void Audio()
            {
                HeroController.instance.gameObject.GetComponent<AudioSource>().PlayOneShot(NAILART, 1f);
            }
            void SummonWaveAir()
            {
                WaveUp();
                WaveDown();
            }
            void WaveGround()
            {
                float side = -HeroController.instance.gameObject.transform.localScale.x;
                float speed = 2f;
                Vector3 scale = new Vector3(0.7f + NailChargeCount * 0.1f, 0.2f + NailChargeCount * 0.1f, 1f);
                var wave = Instantiate(WAVE, HeroController.instance.gameObject.transform.position + new Vector3(0f, -0.8f, 0.01f), new Quaternion());
                wave.transform.localScale = new Vector3(scale.x * side, scale.y, 1f);
                wave.GetComponent<Rigidbody2D>().velocity = new Vector2(speed * side, 0f);
                wave.AddComponent<WaveSpeedUp>();
                wave.transform.Find("Grass").gameObject.Recycle();
                var slash_core = wave.transform.Find("slash_core").gameObject;
                slash_core.transform.Find("Particle_rocks_long (2)").gameObject.Recycle();
                slash_core.transform.Find("Particle Rest").gameObject.Recycle();
                var hurtbox = slash_core.transform.Find("hurtbox").gameObject;
                hurtbox.GetComponent<DamageHero>().damageDealt = 0;
                hurtbox.layer = LayerMask.NameToLayer("Attack");
                SetDamageEnemy(hurtbox, (int)(NailChargeCount * HeroController.instance.playerData.nailDamage * 0.5f), 90f - 90f * side, 1f, AttackTypes.Spell);
            }
            void WaveUp()
            {
                float side = -HeroController.instance.gameObject.transform.localScale.x;
                float speed = 2f;
                Vector3 scale = new Vector3(0.6f + NailChargeCount * 0.1f, 0.1f + NailChargeCount * 0.08f, 1f);
                var wave = Instantiate(WAVE, HeroController.instance.gameObject.transform.position + new Vector3(0f, -0.2f, 0.01f), new Quaternion());
                wave.transform.localScale = new Vector3(scale.x * side, scale.y, 1f);
                wave.transform.eulerAngles = new Vector3(0f, 0f, 23f * side);
                wave.GetComponent<Rigidbody2D>().velocity = new Vector2(speed * side, 0f);
                wave.AddComponent<WaveSpeedUp>();
                wave.transform.Find("Grass").gameObject.Recycle();
                var slash_core = wave.transform.Find("slash_core").gameObject;
                slash_core.transform.Find("Particle_rocks_long (2)").gameObject.Recycle();
                slash_core.transform.Find("Particle Rest").gameObject.Recycle();
                var hurtbox = slash_core.transform.Find("hurtbox").gameObject;
                hurtbox.GetComponent<DamageHero>().damageDealt = 0;
                hurtbox.layer = LayerMask.NameToLayer("Attack");
                SetDamageEnemy(hurtbox, (int)(NailChargeCount * HeroController.instance.playerData.nailDamage * 1f), 90f - 90f * side, 1f, AttackTypes.Spell);
            }
            void WaveDown()
            {
                float side = -HeroController.instance.gameObject.transform.localScale.x;
                float speed = 2f;
                Vector3 scale = new Vector3(0.6f + NailChargeCount * 0.1f, -(0.1f + NailChargeCount * 0.08f), 1f);
                var wave = Instantiate(WAVE, HeroController.instance.gameObject.transform.position + new Vector3(0f, 0.2f, 0.01f), new Quaternion());
                wave.transform.localScale = new Vector3(scale.x * side, scale.y, 1f);
                wave.transform.eulerAngles = new Vector3(0f, 0f, -23f * side);
                wave.GetComponent<Rigidbody2D>().velocity = new Vector2(speed * side, 0f);
                wave.AddComponent<WaveSpeedUp>();
                wave.transform.Find("Grass").gameObject.Recycle();
                var slash_core = wave.transform.Find("slash_core").gameObject;
                slash_core.transform.Find("Particle_rocks_long (2)").gameObject.Recycle();
                slash_core.transform.Find("Particle Rest").gameObject.Recycle();
                var hurtbox = slash_core.transform.Find("hurtbox").gameObject;
                hurtbox.GetComponent<DamageHero>().damageDealt = 0;
                hurtbox.layer = LayerMask.NameToLayer("Attack");
                SetDamageEnemy(hurtbox, (int)(NailChargeCount * HeroController.instance.playerData.nailDamage * 1f), 90f - 90f * side, 1f, AttackTypes.Spell);
            }
            void SummonWave_S()
            {
                float side = -HeroController.instance.gameObject.transform.localScale.x;
                float speed = 5f;
                Vector3 scale = new Vector3(2f, 0.6f, 1f);
                var wave = Instantiate(WAVE, HeroController.instance.gameObject.transform.position + new Vector3(0f, -0.8f, 0f), new Quaternion());
                wave.transform.localScale = new Vector3(scale.x * side, scale.y, 1f);
                wave.GetComponent<Rigidbody2D>().velocity = new Vector2(speed * side, 0f);
                wave.AddComponent<WaveSpeedUp>();
                var slash_core = wave.transform.Find("slash_core").gameObject;
                slash_core.transform.Find("Particle_rocks_long (2)").gameObject.Recycle();
                slash_core.transform.Find("Particle Rest").gameObject.Recycle();
                slash_core.GetComponent<SpriteRenderer>().color = new Color(1f, 0.2f, 0.2f, 1f);
                var hurtbox = slash_core.transform.Find("hurtbox").gameObject;
                hurtbox.GetComponent<DamageHero>().damageDealt = 0;
                hurtbox.layer = LayerMask.NameToLayer("Attack");
                SetDamageEnemy(hurtbox, HeroController.instance.playerData.nailDamage * 4, 90f - 90f * side, 2f, AttackTypes.Spell);
            }
            public void Revenge_S()
            {
                HeroController.instance.gameObject.GetComponent<AudioSource>().PlayOneShot(BLOCKSUCCEED, 1.8f);
                BlockFlashCancel();
                ChangeScale_S();
                Invoke("DamageChange_S", 0.005f);
                Invoke("OneSlash", 0.0001f);
                Invoke("BlockFlash_S", 0.0001f);
                Invoke("Freeze", 0.001f);
                Invoke("Audio", 0.04f);
                Invoke("Freeze_S", 0.06f);
                Invoke("Invuln", 0.07f);
                if(HeroController.instance.playerData.GetInt("health") == 1 && HeroController.instance.playerData.GetBool("equippedCharm_6"))
                {
                    Invoke("Revenge_S_Repeat", 0.2f);
                }
                else
                {
                    Invoke("RecoverScale", 0.2601f);
                    Invoke("AnimRecover", 0.2601f);
                    Invoke("DamageRecover", 0.2601f);
                }
                NailChargeCount = 0;
                Slashed = true;
            }
            public void Revenge_S_Repeat()
            {
                ChangeScale_S();
                BlockReapetTime();
                Invoke("DamageChange_S", 0.005f);
                Invoke("OneSlash", 0.0001f);
                Invoke("Audio", 0.04f);
                Invoke("Freeze_S", 0.06f);
                Invoke("Invuln", 0.07f);
                Invoke("SummonWave_S", 0.05f);
                Invoke("RecoverScale", 0.2601f);
                Invoke("AnimRecover", 0.2601f);
                Invoke("DamageRecover", 0.2601f);
                NailChargeCount = 0;
            }
            public void Revenge()
            {
                HeroController.instance.gameObject.GetComponent<AudioSource>().PlayOneShot(BLOCKSUCCEED, 1.5f);
                ChangeScale();
                if (NailChargeCount < 4)
                {
                    NailChargeCount++;
                }
                if (HeroController.instance.playerData.GetBool("equippedCharm_35"))
                {
                    if(HeroController.instance.cState.onGround == true)
                    {
                        Invoke("WaveGround", 0.1f);
                    }
                    else
                    {
                        Invoke("SummonWaveAir", 0.1f);
                    }
                }
                Invoke("DamageChange", 0.005f);
                Invoke("OneSlash", 0.0001f);
                Invoke("Freeze", 0.0001f);
                Invoke("Invuln", 0.1f);
                Invoke("RecoverScale", 0.2001f);
                Invoke("AnimRecover", 0.2001f);
                Invoke("DamageRecover", 0.2001f);
                SuperNailCharged = false;
                PT.GetComponent<ParticleSystem>().enableEmission = false;
                Slashed = true;
            }
            public void Revenge_W()
            {
                ChangeScale();
                Invoke("DamageChange_W", 0.005f);
                Invoke("OneSlash", 0.0001f);
                Invoke("RecoverScale", 0.2001f);
                Invoke("AnimRecover", 0.2001f);
                Invoke("DamageRecover", 0.2001f);
                SuperNailCharged = false;
                PT.GetComponent<ParticleSystem>().enableEmission = false;
                Slashed = true;
            }
        }
        public class SuperNailCharge : MonoBehaviour
        {
            void Update()
            {
                if (GameManager.instance.inputHandler.inputActions.dreamNail.IsPressed && GameManager.instance.inputHandler.inputActions.dreamNail.HasChanged && NailChargeCount >= 4)
                {
                    gameObject.GetComponent<SuperNailChargePt>().Blast();
                    SuperNailCharged = true;
                    NailChargeCount = 0;
                }
            }
        }
        public class BlockingNailFly : MonoBehaviour
        {
            void Awake()
            {
                GameObject closest = null;
                float closestDistance = Mathf.Infinity;

                foreach (GameObject obj in GameObject.FindObjectsOfType<GameObject>())
                {
                    if (obj != gameObject && obj != HeroController.instance.gameObject && obj.GetComponent<HealthManager>() != null && !obj.name.Contains("Zote Balloon"))
                    {
                        float distance = Vector2.Distance(gameObject.transform.position, obj.transform.position);

                        if (distance < closestDistance)
                        {
                            closest = obj;
                            closestDistance = distance;
                        }
                    }
                }
                if (closest != null)
                {
                    double angle = RadiansToDegrees(Math.Atan((closest.transform.position.y - HeroController.instance.gameObject.transform.position.y) / (closest.transform.position.x - HeroController.instance.gameObject.transform.position.x)));
                    if(closest.transform.position.x - HeroController.instance.gameObject.transform.position.x < 0)
                    {
                        angle += 180f;
                    }
                    float distance = Vector2.Distance(gameObject.transform.position, closest.transform.position);
                    gameObject.GetComponent<Rigidbody2D>().velocity = (closest.transform.position - gameObject.transform.position) * 180f / distance;
                    int damage = PlayerData.instance.nailDamage;
                    if (HeroController.instance.playerData.GetBool("equippedCharm_15"))
                    {
                        damage = (int)(PlayerData.instance.nailDamage * 1.5f);
                    }
                    else
                    {
                        damage = PlayerData.instance.nailDamage;
                    }
                    float HeroDistance = Vector2.Distance(HeroController.instance.gameObject.transform.position, closest.transform.position);
                    SetDamageEnemy(gameObject, damage, (float)angle, 1f / (1f + HeroDistance / 3f), AttackTypes.Spell);
                }
                float speed = (Vector2.Distance(gameObject.GetComponent<Rigidbody2D>().velocity, new Vector2(0, 0)));
                gameObject.transform.localScale = new Vector3(0.8f, 0.8f + speed * 0.008f, 1f);
            }
        }
        public class BlockingWavesSummon : MonoBehaviour
        {
            public void SummonWave()
            {
                float side = -HeroController.instance.gameObject.transform.localScale.x;
                float speed = 24f;
                Vector3 scale = new Vector3((NailChargeCount + 5) / 10, (NailChargeCount + 5) / 10, 1);
                SummonOne(side, speed, scale);
            }
            void SummonOne(float side, float speed, Vector3 scale)
            {
                var wave = Instantiate(WAVE, HeroController.instance.gameObject.transform.position, new Quaternion());
                /*
                wave.transform.localScale = new Vector3(scale.x * side, scale.y, 1f);
                wave.GetComponent<Rigidbody2D>().velocity = new Vector2(speed * side, 0f);
                var slash_core = wave.transform.Find("slash_core").gameObject;
                //slash_core.transform.Find("Particle_rocks_long (2)").gameObject.Recycle();
                //slash_core.transform.Find("Particle Rest").gameObject.Recycle();
                var hurtbox = slash_core.transform.Find("hurtbox").gameObject;
                hurtbox.GetComponent<DamageHero>().damageDealt = 0;
                SetDamageEnemy(hurtbox, NailChargeCount * HeroController.instance.playerData.nailDamage, 90f - 90f * side, 1f, AttackTypes.Spell);
                */
            }
        }
        public class UpDashChangeScale : MonoBehaviour
        {
            public void Change(float angle)
            {
                gameObject.transform.eulerAngles = new Vector3(0f, 0f, angle);
                Invoke("Recover", 0.2f);
            }
            public void Recover()
            {
                gameObject.transform.eulerAngles = new Vector3(0f, 0f, 0f);
            }
        }
        public class DashBlastSummon : MonoBehaviour
        {
            static System.Random random = new System.Random();
            static double R1 => random.NextDouble();
            static double R5 => random.NextDouble();
            void OnTriggerEnter2D(Collider2D collision)
            {
                if (!Dashing_S && HeroController.instance.playerData.GetBool("equippedCharm_35") && collision.GetComponent<HealthManager>() != null && collision.gameObject != HeroController.instance.gameObject)
                {
                    ShadowDashHitCount++;
                    if (ShadowDashHitCount >= 5 - NailChargeCount)
                    {
                        ShadowDashHitCount = 0;
                        if(NailChargeCount <= 4)
                        {
                            NailChargeCount++;
                        }
                        Invoke("SummonNail", 0.15f);
                    }
                }
            }
            void SummonNail()
            {
                float R = (float)R1 - 0.5f;
                var nail = GameObject.Instantiate(NAIL1, HeroController.instance.gameObject.transform.position + new Vector3(R * 25f, 14f, 0), Quaternion.Euler(0f, 0f, 180f));
                nail.tag = "WaitForClean";
                nail.AddComponent<NailRecycle>();
                nail.GetComponent<DamageHero>().damageDealt = 0;
                nail.LocateMyFSM("Control").GetState("Start").AddMethod(() =>
                {
                    nail.transform.Find("Beam").Recycle();
                    nail.transform.Find("Glow").transform.localScale *= 2f;
                    nail.transform.Find("Glow").transform.SetParent(null);
                    nail.LocateMyFSM("Control").FsmVariables.FindFsmFloat("Angle").Value = 0f;
                    nail.LocateMyFSM("Control").SendEvent("FINISHED");
                });
                nail.LocateMyFSM("Control").GetState("Fire").AddMethod(() =>
                {
                    nail.layer = LayerMask.NameToLayer("Attack");
                    nail.transform.localScale = new Vector3(1f, 1.8f, 1f);
                    nail.AddComponent<BlockingNailFly>();
                });
            }
        }
        public class CyclonePlumeSummon : MonoBehaviour
        {
            static System.Random random = new System.Random();
            static double R1 => random.NextDouble();
            static double R5 => random.NextDouble();
            float Timer = 0f;
            void OnTriggerStay2D(Collider2D collision)
            {
                if (HeroController.instance.playerData.GetBool("equippedCharm_35") && collision.GetComponent<HealthManager>() != null)
                {
                    if (Timer <= 0f)
                    {
                        Timer = 0.2f;
                        float scale = 2 - (float)NailChargeCount / 5;
                        SummonBlast(scale,NailChargeCount * 4);
                    }
                    else
                    {
                        Timer -= Time.deltaTime;
                    }
                }
            }
            void SummonBlast(float scaleFactor, int damage)
            {
                GameObject closest = null;
                float closestDistance = Mathf.Infinity;

                foreach (GameObject obj in GameObject.FindObjectsOfType<GameObject>())
                {
                    if (obj != HeroController.instance.gameObject && obj.GetComponent<HealthManager>() != null && !obj.name.Contains("Zote Balloon"))
                    {
                        float distance = Vector2.Distance(HeroController.instance.gameObject.transform.position, obj.transform.position);

                        if (distance < closestDistance)
                        {
                            closest = obj;
                            closestDistance = distance;
                        }
                    }
                }

                double angle1 = R1 * 4d * Math.PI;
                float distance1 = (float)R5 * NailChargeCount;
                var blast = Instantiate(BLAST, closest.transform.position + new Vector3((float)Math.Sin(angle1) * distance1, (float)Math.Cos(angle1) * distance1, 0f), Quaternion.Euler(0f, 0f, 0f));
                blast.tag = "WaitForClean";
                blast.AddComponent<SoonRecycle>();
                var blastchild = blast.transform.Find("Blast").gameObject;
                blastchild.transform.localScale /= scaleFactor;
                blastchild.transform.Find("hero_damager").GetComponent<DamageHero>().damageDealt = 0;
                blastchild.transform.Find("hero_damager").gameObject.layer = LayerMask.NameToLayer("Attack");
                blastchild.AddComponent<BlastSpeed2>();
                blastchild.GetComponent<Animator>().speed = 3;
                blastchild.SetActive(true);
                SetDamageEnemy(blastchild.transform.Find("hero_damager").gameObject, damage, 90f + HeroController.instance.gameObject.transform.localScale.x * 90f, 0.1f, AttackTypes.Spell);
            }
        }
        public class SuperNailChargePt: MonoBehaviour
        {
            public void Awake()
            {
                Pt1Summon();
                Pt2Summon();
            }
            void Pt1Summon()
            {
                var nail = GameObject.Instantiate(NAIL1, HeroController.instance.gameObject.transform.position + new Vector3(0f, 100f, 0), Quaternion.Euler(0f, 0f, 0f));
                nail.tag = "WaitForClean";
                nail.transform.Find("Beam").Recycle();
                nail.SetActive(false);
                var dribble = nail.transform.Find("Dribble L").gameObject;
                dribble.transform.SetParent(HeroController.instance.gameObject.transform);
                PT = dribble;
                PT.GetComponent<ParticleSystem>().emissionRate = 160 * Time.deltaTime / 0.02f;
                PT.GetComponent<ParticleSystem>().startSpeed = 1;
                PT.GetComponent<ParticleSystem>().enableEmission = false;
                PT.transform.position = HeroController.instance.gameObject.transform.position + new Vector3(0f, -0.3f, 0.01f);
                PT.transform.localScale = new Vector3(1f, 1.3f, 0.7f);
            }
            void Pt2Summon()
            {
                var nail = GameObject.Instantiate(NAIL1, HeroController.instance.gameObject.transform.position + new Vector3(0f, 100f, 0), Quaternion.Euler(0f, 0f, 0f));
                nail.tag = "WaitForClean";
                nail.transform.Find("Beam").Recycle();
                nail.SetActive(false);
                var dribble = nail.transform.Find("Dribble L").gameObject;
                dribble.transform.SetParent(HeroController.instance.gameObject.transform);
                PT2 = dribble;
                PT2.GetComponent<ParticleSystem>().emissionRate = 500000 * Time.deltaTime;
                PT2.GetComponent<ParticleSystem>().startSpeed = 7;
                PT2.GetComponent<ParticleSystem>().startSize *= 1.5f;
                PT2.GetComponent<ParticleSystem>().enableEmission = false;
                PT2.transform.position = HeroController.instance.gameObject.transform.position + new Vector3(0f, -0.3f, 0.01f);
                PT2.transform.localScale = new Vector3(1f, 1.3f, 0.7f);
            }
            public void Blast()
            {
                PT.GetComponent<ParticleSystem>().enableEmission = true;
                PT2.GetComponent<ParticleSystem>().enableEmission = true;
                Invoke("Pt2Close", 0.1f);
            }
            void Pt2Close()
            {
                PT2.GetComponent<ParticleSystem>().enableEmission = false;
            }
        }
        public class SuperSpell : MonoBehaviour
        {
            int NailEnhanceCount = 0;
            int BlastEnhanceCount = 0;
            int PlumeEnhanceCount = 0;
            public void NailEnhance()
            {
                if(NailEnhanceCount <= 2)
                {
                    NailEnhanceCount++;
                }
                else
                {
                    NailEnhanceCount = 0;
                    CanSuperFireBall = true;
                }
                CanFireBall = true;
                CancelInvoke("NailEnhanceEnd");
                Invoke("NailEnhanceEnd", 0.5f);
            }
            public void NailEnhanceEnd()
            {
                NailEnhanceCount = 0;
                CanFireBall = false;
                CanSuperFireBall = false;
            }
            public void BlastEnhance()
            {
                if(BlastEnhanceCount <= 1)
                {
                    BlastEnhanceCount++;
                }
                else
                {
                    BlastEnhanceCount = 0;
                    CanSuperScream = true;
                }
                CanScream = true;
                CancelInvoke("BlastEnhanceEnd");
                Invoke("BlastEnhanceEnd", 1f);
            }
            public void BlastEnhanceEnd()
            {
                BlastEnhanceCount = 0;
                CanScream = false;
                CanSuperScream = false;
            }
            public void PlumeEnhance()
            {
                if(PlumeEnhanceCount <= 0 && !CanSuperQuake)
                {
                    PlumeEnhanceCount++;
                }
                else
                {
                    PlumeEnhanceCount = 0;
                    CanSuperQuake = true;
                }
                CanQuake = true;
                CancelInvoke("PlumeEnhanceEnd");
                Invoke("PlumeEnhanceEnd", 1f);
            }
            public void PlumeEnhanceEnd()
            {
                PlumeEnhanceCount = 0;
                CanQuake = false;
                CanSuperQuake = false;
            }
        }
        public override List<(string, string)> GetPreloadNames()
        {
            return new List<(string, string)>
            {
                ("GG_Radiance","Boss Control"),
                ("GG_Hollow_Knight","Battle Scene/HK Prime"),
                ("GG_Hollow_Knight","Battle Scene/Focus Blasts"),
                ("GG_Soul_Master","Mage Lord"),
                ("GG_Traitor_Lord","Battle Scene/Wave 3/Mantis Traitor Lord")
            };
        }
        public void EnterGame()
        {
            if (settings_.on)
            {
                ModHooks.LanguageGetHook += ModHooks_LanguageGetHook;
                On.PlayMakerFSM.Start += PlayMakerFSM_Start;
                On.HealthManager.TakeDamage += HealthManager_TakeDamage;
                On.HeroController.TakeDamage += HeroController_TakeDamage;
                On.HeroController.Attack += HeroController_Attack;
                On.HeroController.HeroDash += HeroController_HeroDash;
                On.HeroController.Dash += HeroController_Dash;
                On.HeroController.SoulGain += HeroController_SoulGain;
                On.NailSlash.StartSlash += NailSlash_StartSlash;
                On.GameManager.FreezeMoment_int += GameManager_FreezeMoment_int;
            }
            else
            {
                ModHooks.LanguageGetHook -= ModHooks_LanguageGetHook;
                On.PlayMakerFSM.Start -= PlayMakerFSM_Start;
                On.HealthManager.TakeDamage -= HealthManager_TakeDamage;
                On.HeroController.TakeDamage -= HeroController_TakeDamage;
                On.HeroController.Attack -= HeroController_Attack;
                On.HeroController.HeroDash -= HeroController_HeroDash;
                On.HeroController.Dash -= HeroController_Dash;
                On.HeroController.SoulGain -= HeroController_SoulGain;
                On.NailSlash.StartSlash -= NailSlash_StartSlash;
                On.GameManager.FreezeMoment_int -= GameManager_FreezeMoment_int;
            }
        }
        public override void Initialize(Dictionary<string, Dictionary<string, GameObject>> preloadedObjects)
        {
            On.GameManager.LoadGame += GameManager_LoadGame;

            var radiance = preloadedObjects["GG_Radiance"]["Boss Control"].transform.Find("Absolute Radiance").gameObject;
            var burst = radiance.transform.Find("Eye Beam Glow").gameObject.transform.Find("Burst 1").gameObject;
            BEAM = burst.transform.Find("Radiant Beam").gameObject;
            var nail = radiance.LocateMyFSM("Attack Commands").GetState("CW Spawn").GetAction<SpawnObjectFromGlobalPool>().gameObject.Value;
            NAIL = nail;
            DREAMPT = radiance.transform.Find("Eye Final Pt").gameObject;

            var MageLord = preloadedObjects["GG_Soul_Master"]["Mage Lord"].gameObject;

            var hk = preloadedObjects["GG_Hollow_Knight"]["Battle Scene/HK Prime"].gameObject;
            var blasts = preloadedObjects["GG_Hollow_Knight"]["Battle Scene/Focus Blasts"].gameObject;
            BLAST = blasts.transform.Find("HK Prime Blast").gameObject;
            NAIL1 = hk.LocateMyFSM("Control").GetState("SmallShot LowHigh").GetAction<FlingObjectsFromGlobalPoolTime>().gameObject.Value;
            GLOW = NAIL1.transform.Find("Dribble L").gameObject;
            TENTRILS = hk.transform.Find("Tendrils").gameObject;

            PLUME = hk.LocateMyFSM("Control").GetState("Plume Gen").GetAction<SpawnObjectFromGlobalPool>().gameObject.Value as GameObject;
            BIGBLASTHIT = hk.transform.Find("Focus Hit").gameObject;
            BIGBLAST = hk.transform.Find("Focus Blast").gameObject;

            var tl = preloadedObjects["GG_Traitor_Lord"]["Battle Scene/Wave 3/Mantis Traitor Lord"].gameObject;
            WAVE = tl.LocateMyFSM("Mantis").GetState("Waves").GetAction<SpawnObjectFromGlobalPool>(0).gameObject.Value;

            var audioclip1 = hk.LocateMyFSM("Control").GetState("Ball Up").GetAction<AudioPlayerOneShotSingle>().audioClip.Value as AudioClip;
            var audioclip2 = hk.LocateMyFSM("Control").GetState("Focus Burst").GetAction<AudioPlayerOneShotSingle>().audioClip.Value as AudioClip;
            var audioclip3 = hk.LocateMyFSM("Control").GetState("Counter Stance").GetAction<AudioPlayerOneShotSingle>().audioClip.Value as AudioClip;
            var audioclip4 = hk.LocateMyFSM("Control").GetState("CS Antic").GetAction<AudioPlayerOneShotSingle>().audioClip.Value as AudioClip;
            var audioclip5 = hk.LocateMyFSM("Control").GetState("Tele Out").GetAction<AudioPlayerOneShotSingle>().audioClip.Value as AudioClip;
            var audioclip6 = hk.LocateMyFSM("Control").GetState("Plume Up").GetAction<AudioPlayerOneShotSingle>().audioClip.Value as AudioClip;
            BALLUP = audioclip1;
            BALLEXPLODE = audioclip2;
            BLOCKING = audioclip3;
            BLOCKSUCCEED = audioclip4;
            TELE = audioclip5;
            PLUMEUP = audioclip6;
        }

        private void GameManager_LoadGame(On.GameManager.orig_LoadGame orig, GameManager self, int saveSlot, Action<bool> callback)
        {
            EnterGame();
            orig(self, saveSlot, callback);
        }

        private void HeroController_SoulGain(On.HeroController.orig_SoulGain orig, HeroController self)
        {
            var this_ = self.Reflect();

            int num;
            if (this_.playerData.GetInt("MPCharge") < this_.playerData.GetInt("maxMP"))
            {
                num = 11;
                if (this_.playerData.GetBool("equippedCharm_20"))
                {
                    num += 3;
                }
                if (this_.playerData.GetBool("equippedCharm_21"))
                {
                    num += 8;
                }
            }
            else
            {
                num = 6;
                if (this_.playerData.GetBool("equippedCharm_20"))
                {
                    num += 2;
                }
                if (this_.playerData.GetBool("equippedCharm_21"))
                {
                    num += 6;
                }
            }

            if(this_.playerData.GetBool("equippedCharm_33"))
            {
                num = (int)(num / 2.5f);
            }

            int @int = this_.playerData.GetInt("MPReserve");
            this_.playerData.AddMPCharge(num);
            GameCameras.instance.soulOrbFSM.SendEvent("MP GAIN");
            if (this_.playerData.GetInt("MPReserve") != @int)
            {
                this_.gm.soulVessel_fsm.SendEvent("MP RESERVE UP");
            }
        }

        private void HeroController_Dash(On.HeroController.orig_Dash orig, HeroController self)
        {
            var this_ = self.Reflect();
            this_.AffectedByGravity(false);
            this_.ResetHardLandingTimer();
            if (this_.dash_timer > this_.DASH_TIME)
            {
                this_.FinishedDashing();
                return;
            }
            Vector2 vector = this_.OrigDashVector();
            float num;
            if (this_.playerData.GetBool("equippedCharm_16") && this_.cState.shadowDashing)
            {
                num = this_.DASH_SPEED_SHARP;
            }
            else
            {
                num = this_.DASH_SPEED;
            }
            if(Dashing_S)
            {
                this_.rb2d.velocity = DashVec;
            }
            else
            {
                if (dashingUp)
                {
                    this_.rb2d.velocity = new Vector2(0f, num * 0.6f);
                }
                else
                {
                    this_.rb2d.velocity = vector;
                }
            }
            this_.dash_timer += Time.deltaTime;
        }

        private Vector2 ModHooks_DashVectorHook(Vector2 arg)
        {
            if(HeroController.instance.gameObject.GetComponent<NewDashSlash>().LongDistance == true)
            {
                return arg * 1.5f;
            }
            else
            {
                return arg;
            }
        }

        private void HeroController_HeroDash(On.HeroController.orig_HeroDash orig, HeroController self)
        {
            var this_ = self.Reflect();
            float Angle = 0f;
            GameObject closest = null;
            float closestDistance = Mathf.Infinity;

            foreach (GameObject obj in GameObject.FindObjectsOfType<GameObject>())
            {
                if (obj != HeroController.instance.gameObject && obj.GetComponent<HealthManager>() != null && !obj.name.Contains("Zote Balloon"))
                {
                    float distance = Vector2.Distance(HeroController.instance.gameObject.transform.position, obj.transform.position);

                    if (distance < closestDistance)
                    {
                        closest = obj;
                        closestDistance = distance;
                    }
                }
            }
            if (!this_.cState.onGround && !this_.inAcid)
            {
                this_.airDashed = true;
            }
            this_.ResetAttacksDash();
            this_.CancelBounce();
            this_.audioCtrl.StopSound(HeroSounds.FOOTSTEPS_RUN);
            this_.audioCtrl.StopSound(HeroSounds.FOOTSTEPS_WALK);
            this_.audioCtrl.PlaySound(HeroSounds.DASH);
            this_.ResetLook();
            this_.cState.recoiling = false;
            if (this_.cState.wallSliding)
            {
                this_.FlipSprite();
            }
            else if (this_.inputHandler.inputActions.right.IsPressed)
            {
                this_.FaceRight();
            }
            else if (this_.inputHandler.inputActions.left.IsPressed)
            {
                this_.FaceLeft();
            }
            this_.cState.dashing = true;
            this_.dashQueueSteps = 0;
            HeroActions inputActions = this_.inputHandler.inputActions;
            if (Dashing_S)
            {
                if (closest != null)
                {
                    float num;
                    if (this_.playerData.GetBool("equippedCharm_16") && this_.cState.shadowDashing)
                    {
                        num = this_.DASH_SPEED_SHARP;
                    }
                    else
                    {
                        num = this_.DASH_SPEED;
                    }
                    Vector3 HeroPosi = HeroController.instance.gameObject.transform.position;
                    Vector3 ClosestPosi = closest.transform.position;
                    float distance1 = Vector2.Distance(HeroPosi, ClosestPosi);

                    //DashVec = new Vector2(ClosestPosi.x - HeroPosi.x, ClosestPosi.y - HeroPosi.y) * num * 2.5f / distance1;
                    double angleRad = Math.Atan(ClosestPosi.y - HeroPosi.y) / (ClosestPosi.x - HeroPosi.x);
                    if (ClosestPosi.x < HeroPosi.x)
                    {
                        angleRad += Math.PI;
                    }
                    float angle = (float)RadiansToDegrees(angleRad);
                    DashVec = new Vector2((float)Math.Cos(angleRad) * num * 2.5f, (float)Math.Sin(angleRad) * num * 2.5f);
                    this_.dashBurst.transform.localPosition = new Vector3(-0.07f, 3.74f, 0.01f);
                    this_.dashBurst.transform.localEulerAngles = new Vector3(0f, 0f, 90f);
                    this_.dashingDown = true;
                    dashingUp = true;
                    Angle = angle;
                    HeroController.instance.gameObject.GetComponent<UpDashChangeScale>().Change(angle - 90);
                }
                else
                {
                    if (inputActions.down.IsPressed && !this_.cState.onGround && this_.playerData.GetBool("equippedCharm_31") && !inputActions.left.IsPressed && !inputActions.right.IsPressed)
                    {
                        this_.dashBurst.transform.localPosition = new Vector3(-0.07f, 3.74f, 0.01f);
                        this_.dashBurst.transform.localEulerAngles = new Vector3(0f, 0f, 90f);
                        this_.dashingDown = true;
                        dashingUp = false;
                    }
                    else if (inputActions.up.IsPressed && this_.playerData.GetBool("equippedCharm_31") && !inputActions.left.IsPressed && !inputActions.right.IsPressed)
                    {
                        this_.dashBurst.transform.localPosition = new Vector3(-0.07f, 3.74f, 0.01f);
                        this_.dashBurst.transform.localEulerAngles = new Vector3(0f, 0f, 90f);
                        this_.dashingDown = true;
                        dashingUp = true;
                        HeroController.instance.gameObject.GetComponent<UpDashChangeScale>().Change(180f);
                    }
                    else
                    {
                        this_.dashBurst.transform.localPosition = new Vector3(4.11f, -0.55f, 0.001f);
                        this_.dashBurst.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
                        this_.dashingDown = false;
                        dashingUp = false;
                    }
                    Vector2 vector = this_.OrigDashVector();
                    float num;
                    if (this_.playerData.GetBool("equippedCharm_16") && this_.cState.shadowDashing)
                    {
                        num = this_.DASH_SPEED_SHARP;
                    }
                    else
                    {
                        num = this_.DASH_SPEED;
                    }
                    if (dashingUp)
                    {
                        DashVec = new Vector2(0f, num * 0.6f);
                    }
                    else
                    {
                        DashVec = vector;
                    }
                }
            }
            else
            {
                if (inputActions.down.IsPressed && !this_.cState.onGround && this_.playerData.GetBool("equippedCharm_31") && !inputActions.left.IsPressed && !inputActions.right.IsPressed)
                {
                    this_.dashBurst.transform.localPosition = new Vector3(-0.07f, 3.74f, 0.01f);
                    this_.dashBurst.transform.localEulerAngles = new Vector3(0f, 0f, 90f);
                    this_.dashingDown = true;
                    dashingUp = false;
                }
                else if (inputActions.up.IsPressed && this_.playerData.GetBool("equippedCharm_31") && !inputActions.left.IsPressed && !inputActions.right.IsPressed)
                {
                    this_.dashBurst.transform.localPosition = new Vector3(-0.07f, 3.74f, 0.01f);
                    this_.dashBurst.transform.localEulerAngles = new Vector3(0f, 0f, 90f);
                    this_.dashingDown = true;
                    dashingUp = true;
                    HeroController.instance.gameObject.GetComponent<UpDashChangeScale>().Change(180f);
                }
                else
                {
                    this_.dashBurst.transform.localPosition = new Vector3(4.11f, -0.55f, 0.001f);
                    this_.dashBurst.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
                    this_.dashingDown = false;
                    dashingUp = false;
                }
            }
            if (this_.playerData.GetBool("equippedCharm_31"))
            {
                this_.dashCooldownTimer = this_.DASH_COOLDOWN_CH;
            }
            else
            {
                this_.dashCooldownTimer = this_.DASH_COOLDOWN;
            }
            if (this_.playerData.GetBool("hasShadowDash") && this_.shadowDashTimer <= 0f)
            {
                this_.shadowDashTimer = this_.SHADOW_DASH_COOLDOWN;
                this_.cState.shadowDashing = true;
                if (this_.playerData.GetBool("equippedCharm_16"))
                {
                    this_.audioSource.PlayOneShot(this_.sharpShadowClip, 1f);
                    this_.sharpShadowPrefab.SetActive(true);
                }
                else
                {
                    this_.audioSource.PlayOneShot(this_.shadowDashClip, 1f);
                }
            }
            else if(DashDashing == true)
            {
                DashDashing = false;
                this_.shadowDashTimer = this_.SHADOW_DASH_COOLDOWN;
                this_.cState.shadowDashing = true;
                if (this_.playerData.GetBool("equippedCharm_16"))
                {
                    this_.audioSource.PlayOneShot(this_.sharpShadowClip, 1f);
                    this_.sharpShadowPrefab.SetActive(true);
                }
                else
                {
                    this_.audioSource.PlayOneShot(this_.shadowDashClip, 1f);
                }
            }
            if (this_.cState.shadowDashing)
            {
                if(Dashing_S && closest != null)
                {
                    this_.dashEffect = this_.shadowdashDownBurstPrefab.Spawn(new Vector3(this_.transform.position.x + (float)Math.Cos(DegreesToRadians(Angle + 180f)) * 3.5f, this_.transform.position.y + (float)Math.Sin(DegreesToRadians(Angle + 180f)) * 3.5f, this_.transform.position.z + 0.00101f));
                    this_.dashEffect.transform.localEulerAngles = new Vector3(0f, 0f, Angle + 180f);
                }
                else
                {
                    if (this_.dashingDown)
                    {
                        if (dashingUp)
                        {
                            this_.dashEffect = this_.shadowdashDownBurstPrefab.Spawn(new Vector3(this_.transform.position.x, this_.transform.position.y - 3.5f, this_.transform.position.z + 0.00101f));
                            this_.dashEffect.transform.localEulerAngles = new Vector3(0f, 0f, -90f);
                        }
                        else
                        {
                            this_.dashEffect = this_.shadowdashDownBurstPrefab.Spawn(new Vector3(this_.transform.position.x, this_.transform.position.y + 3.5f, this_.transform.position.z + 0.00101f));
                            this_.dashEffect.transform.localEulerAngles = new Vector3(0f, 0f, 90f);
                        }
                    }
                    else if (dashingUp)
                    {
                        this_.dashEffect = this_.shadowdashDownBurstPrefab.Spawn(new Vector3(this_.transform.position.x, this_.transform.position.y - 3.5f, this_.transform.position.z + 0.00101f));
                        this_.dashEffect.transform.localEulerAngles = new Vector3(0f, 0f, -90f);
                    }
                    else if (this_.transform.localScale.x > 0f)
                    {
                        this_.dashEffect = this_.shadowdashBurstPrefab.Spawn(new Vector3(this_.transform.position.x + 5.21f, this_.transform.position.y - 0.58f, this_.transform.position.z + 0.00101f));
                        this_.dashEffect.transform.localScale = new Vector3(1.919591f, this_.dashEffect.transform.localScale.y, this_.dashEffect.transform.localScale.z);
                    }
                    else
                    {
                        this_.dashEffect = this_.shadowdashBurstPrefab.Spawn(new Vector3(this_.transform.position.x - 5.21f, this_.transform.position.y - 0.58f, this_.transform.position.z + 0.00101f));
                        this_.dashEffect.transform.localScale = new Vector3(-1.919591f, this_.dashEffect.transform.localScale.y, this_.dashEffect.transform.localScale.z);
                    }
                }
                this_.shadowRechargePrefab.SetActive(true);
                FSMUtility.LocateFSM(this_.shadowRechargePrefab, "Recharge Effect").SendEvent("RESET");
                this_.shadowdashParticlesPrefab.GetComponent<ParticleSystem>().enableEmission = true;
                VibrationManager.PlayVibrationClipOneShot(this_.shadowDashVibration, null, false, "");
                this_.shadowRingPrefab.Spawn(this_.transform.position);
            }
            else
            {
                this_.dashBurst.SendEvent("PLAY");
                this_.dashParticlesPrefab.GetComponent<ParticleSystem>().enableEmission = true;
                VibrationManager.PlayVibrationClipOneShot(this_.dashVibration, null, false, "");
            }
            if (this_.cState.onGround && !this_.cState.shadowDashing)
            {
                this_.dashEffect = this_.backDashPrefab.Spawn(this_.transform.position);
                this_.dashEffect.transform.localScale = new Vector3(this_.transform.localScale.x * -1f, this_.transform.localScale.y, this_.transform.localScale.z);
            }
        }

        private void GameManager_FreezeMoment_int(On.GameManager.orig_FreezeMoment_int orig, GameManager self, int type)
        {
            if (type == 0)
            {
                self.StartCoroutine(self.FreezeMoment(0.01f, 0.35f, 0.1f, 0f));
            }
            else if (type == 1)
            {
                self.StartCoroutine(self.FreezeMoment(0.04f, 0.03f, 0.04f, 0f));
            }
            else if (type == 2)
            {
                self.StartCoroutine(self.FreezeMoment(0.25f, 2f, 0.25f, 0.15f));
            }
            else if (type == 3)
            {
                self.StartCoroutine(self.FreezeMoment(0.01f, 0.25f, 0.1f, 0f));
            }
            if (type == 4)
            {
                self.StartCoroutine(self.FreezeMoment(0.01f, 0.25f, 0.1f, 0f));
            }
            if (type == 5)
            {
                self.StartCoroutine(self.FreezeMoment(0.01f, 0.25f, 0.1f, 0f));
            }
            if (type == 6)
            {
                self.StartCoroutine(self.FreezeMoment(0.01f, 0.5f, 0.1f, 0f));
            }
            if (type == 7)
            {
                self.StartCoroutine(self.FreezeMoment(0.5f, 0.25f, 0.1f, 0f));
            }
        }

        private void HeroController_Attack(On.HeroController.orig_Attack orig, HeroController self, AttackDirection attackDir)
        {
            var this_ = self.Reflect();
            if(!this_.playerData.GetBool("equippedCharm_15") || CanSlash == true)
            {
                if (Time.timeSinceLevelLoad - this_.altAttackTime > this_.ALT_ATTACK_RESET)
                {
                    this_.cState.altAttack = false;
                }
                this_.cState.attacking = true;
                if (this_.playerData.GetBool("equippedCharm_32"))
                {
                    this_.attackDuration = this_.ATTACK_DURATION_CH;
                }
                else
                {
                    this_.attackDuration = this_.ATTACK_DURATION;
                }
                if (this_.cState.wallSliding)
                {
                    this_.wallSlashing = true;
                    this_.slashComponent = this_.wallSlash;
                    this_.slashFsm = this_.wallSlashFsm;
                }
                else
                {
                    this_.wallSlashing = false;
                    if (attackDir == AttackDirection.normal)
                    {
                        if (!this_.cState.altAttack)
                        {
                            this_.slashComponent = this_.normalSlash;
                            this_.slashFsm = this_.normalSlashFsm;
                            this_.cState.altAttack = true;
                        }
                        else
                        {
                            this_.slashComponent = this_.alternateSlash;
                            this_.slashFsm = this_.alternateSlashFsm;
                            this_.cState.altAttack = false;
                        }
                        /*
                        if (this_.playerData.GetBool("equippedCharm_35"))
                        {
                            if ((this_.playerData.GetInt("health") == this_.playerData.GetInt("maxHealth") && !this_.playerData.GetBool("equippedCharm_27")) || (this_.joniBeam && this_.playerData.GetBool("equippedCharm_27")))
                            {
                                if (self.transform.localScale.x < 0f)
                                {
                                    this_.grubberFlyBeam = this_.grubberFlyBeamPrefabR.Spawn(self.transform.position);
                                }
                                else
                                {
                                    this_.grubberFlyBeam = this_.grubberFlyBeamPrefabL.Spawn(self.transform.position);
                                }
                                if (this_.playerData.GetBool("equippedCharm_13"))
                                {
                                    this_.grubberFlyBeam.transform.SetScaleY(this_.MANTIS_CHARM_SCALE);
                                }
                                else
                                {
                                    this_.grubberFlyBeam.transform.SetScaleY(1f);
                                }
                            }
                            if (this_.playerData.GetInt("health") == 1 && this_.playerData.GetBool("equippedCharm_6") && this_.playerData.GetInt("healthBlue") < 1)
                            {
                                if (self.transform.localScale.x < 0f)
                                {
                                    this_.grubberFlyBeam = this_.grubberFlyBeamPrefabR_fury.Spawn(self.transform.position);
                                }
                                else
                                {
                                    this_.grubberFlyBeam = this_.grubberFlyBeamPrefabL_fury.Spawn(self.transform.position);
                                }
                                if (this_.playerData.GetBool("equippedCharm_13"))
                                {
                                    this_.grubberFlyBeam.transform.SetScaleY(this_.MANTIS_CHARM_SCALE);
                                }
                                else
                                {
                                    this_.grubberFlyBeam.transform.SetScaleY(1f);
                                }
                            }
                        }*/
                    }
                    else if (attackDir == AttackDirection.upward)
                    {
                        this_.slashComponent = this_.upSlash;
                        this_.slashFsm = this_.upSlashFsm;
                        this_.cState.upAttacking = true;
                        /*
                        if (this_.playerData.GetBool("equippedCharm_35"))
                        {
                            if ((this_.playerData.GetInt("health") == this_.playerData.GetInt("maxHealth") && !this_.playerData.GetBool("equippedCharm_27")) || (this_.joniBeam && this_.playerData.GetBool("equippedCharm_27")))
                            {
                                this_.grubberFlyBeam = this_.grubberFlyBeamPrefabU.Spawn(self.transform.position);
                                this_.grubberFlyBeam.transform.SetScaleY(self.transform.localScale.x);
                                this_.grubberFlyBeam.transform.localEulerAngles = new Vector3(0f, 0f, 270f);
                                if (this_.playerData.GetBool("equippedCharm_13"))
                                {
                                    this_.grubberFlyBeam.transform.SetScaleY(this_.grubberFlyBeam.transform.localScale.y * this_.MANTIS_CHARM_SCALE);
                                }
                            }
                            if (this_.playerData.GetInt("health") == 1 && this_.playerData.GetBool("equippedCharm_6") && this_.playerData.GetInt("healthBlue") < 1)
                            {
                                this_.grubberFlyBeam = this_.grubberFlyBeamPrefabU_fury.Spawn(self.transform.position);
                                this_.grubberFlyBeam.transform.SetScaleY(self.transform.localScale.x);
                                this_.grubberFlyBeam.transform.localEulerAngles = new Vector3(0f, 0f, 270f);
                                if (this_.playerData.GetBool("equippedCharm_13"))
                                {
                                    this_.grubberFlyBeam.transform.SetScaleY(this_.grubberFlyBeam.transform.localScale.y * this_.MANTIS_CHARM_SCALE);
                                }
                            }
                        }*/
                    }
                    else if (attackDir == AttackDirection.downward)
                    {
                        this_.slashComponent = this_.downSlash;
                        this_.slashFsm = this_.downSlashFsm;
                        this_.cState.downAttacking = true;
                        /*
                        if (this_.playerData.GetBool("equippedCharm_35"))
                        {
                            if ((this_.playerData.GetInt("health") == this_.playerData.GetInt("maxHealth") && !this_.playerData.GetBool("equippedCharm_27")) || (this_.joniBeam && this_.playerData.GetBool("equippedCharm_27")))
                            {
                                this_.grubberFlyBeam = this_.grubberFlyBeamPrefabD.Spawn(self.transform.position);
                                this_.grubberFlyBeam.transform.SetScaleY(self.transform.localScale.x);
                                this_.grubberFlyBeam.transform.localEulerAngles = new Vector3(0f, 0f, 90f);
                                if (this_.playerData.GetBool("equippedCharm_13"))
                                {
                                    this_.grubberFlyBeam.transform.SetScaleY(this_.grubberFlyBeam.transform.localScale.y * this_.MANTIS_CHARM_SCALE);
                                }
                            }
                            if (this_.playerData.GetInt("health") == 1 && this_.playerData.GetBool("equippedCharm_6") && this_.playerData.GetInt("healthBlue") < 1)
                            {
                                this_.grubberFlyBeam = this_.grubberFlyBeamPrefabD_fury.Spawn(self.transform.position);
                                this_.grubberFlyBeam.transform.SetScaleY(self.transform.localScale.x);
                                this_.grubberFlyBeam.transform.localEulerAngles = new Vector3(0f, 0f, 90f);
                                if (this_.playerData.GetBool("equippedCharm_13"))
                                {
                                    this_.grubberFlyBeam.transform.SetScaleY(this_.grubberFlyBeam.transform.localScale.y * this_.MANTIS_CHARM_SCALE);
                                }
                            }
                        }
                        */
                    }
                }
                if (this_.cState.wallSliding)
                {
                    if (this_.cState.facingRight)
                    {
                        this_.slashFsm.FsmVariables.GetFsmFloat("direction").Value = 180f;
                    }
                    else
                    {
                        this_.slashFsm.FsmVariables.GetFsmFloat("direction").Value = 0f;
                    }
                }
                else if (attackDir == AttackDirection.normal && this_.cState.facingRight)
                {
                    this_.slashFsm.FsmVariables.GetFsmFloat("direction").Value = 0f;
                }
                else if (attackDir == AttackDirection.normal && !this_.cState.facingRight)
                {
                    this_.slashFsm.FsmVariables.GetFsmFloat("direction").Value = 180f;
                }
                else if (attackDir == AttackDirection.upward)
                {
                    this_.slashFsm.FsmVariables.GetFsmFloat("direction").Value = 90f;
                }
                else if (attackDir == AttackDirection.downward)
                {
                    this_.slashFsm.FsmVariables.GetFsmFloat("direction").Value = 270f;
                }
                this_.altAttackTime = Time.timeSinceLevelLoad;
                if (!this_.cState.attacking)
                {
                    return;
                }
                this_.slashComponent.StartSlash();
                if (this_.playerData.GetBool("equippedCharm_38"))
                {
                    this_.fsm_orbitShield.SendEvent("SLASH");
                }
            }
        }

        private void HeroController_TakeDamage(On.HeroController.orig_TakeDamage orig, HeroController self, GameObject go, CollisionSide damageSide, int damageAmount, int hazardType)
        {
            var this_ = self.Reflect();

            bool spawnDamageEffect = true;
            if (damageAmount > 0)
            {
                HeroController.instance.gameObject.GetComponent<FocusUp>().FocusCancel();

                if (BossSceneController.IsBossScene)
                {
                    int bossLevel = BossSceneController.Instance.BossLevel;
                    if (bossLevel != 1)
                    {
                        if (bossLevel == 2)
                        {
                            damageAmount = 9999;
                        }
                    }
                    else
                    {
                        damageAmount *= 2;
                    }
                }
                if (this_.CanTakeDamage() || Blocking == true)
                {
                    if (this_.damageMode == DamageMode.HAZARD_ONLY && hazardType == 1)
                    {
                        return;
                    }
                    if (this_.cState.shadowDashing && hazardType == 1)
                    {
                        return;
                    }
                    if (this_.parryInvulnTimer > 0f && hazardType == 1)
                    {
                        return;
                    }
                    VibrationMixer mixer = VibrationManager.GetMixer();
                    if (mixer != null)
                    {
                        mixer.StopAllEmissionsWithTag("heroAction");
                    }
                    bool flag = false;
                    if (this_.carefreeShieldEquipped && hazardType == 1)
                    {
                        if (this_.hitsSinceShielded > 7)
                        {
                            this_.hitsSinceShielded = 7;
                        }
                        switch (this_.hitsSinceShielded)
                        {
                            case 1:
                                if ((float)UnityEngine.Random.Range(1, 100) <= 10f)
                                {
                                    flag = true;
                                }
                                break;
                            case 2:
                                if ((float)UnityEngine.Random.Range(1, 100) <= 20f)
                                {
                                    flag = true;
                                }
                                break;
                            case 3:
                                if ((float)UnityEngine.Random.Range(1, 100) <= 30f)
                                {
                                    flag = true;
                                }
                                break;
                            case 4:
                                if ((float)UnityEngine.Random.Range(1, 100) <= 50f)
                                {
                                    flag = true;
                                }
                                break;
                            case 5:
                                if ((float)UnityEngine.Random.Range(1, 100) <= 70f)
                                {
                                    flag = true;
                                }
                                break;
                            case 6:
                                if ((float)UnityEngine.Random.Range(1, 100) <= 80f)
                                {
                                    flag = true;
                                }
                                break;
                            case 7:
                                if ((float)UnityEngine.Random.Range(1, 100) <= 90f)
                                {
                                    flag = true;
                                }
                                break;
                            default:
                                flag = false;
                                break;
                        }
                        if (flag)
                        {
                            this_.hitsSinceShielded = 0;
                            this_.carefreeShield.SetActive(true);
                            damageAmount = 0;
                            spawnDamageEffect = false;
                        }
                        else
                        {
                            this_.hitsSinceShielded++;
                        }
                    }
                    if (this_.playerData.GetBool("equippedCharm_5") && this_.playerData.GetInt("blockerHits") > 0 && hazardType == 1 && this_.cState.focusing && !flag && Blocking == false)
                    {
                        this_.proxyFSM.SendEvent("HeroCtrl-TookBlockerHit");
                        this_.audioSource.PlayOneShot(this_.blockerImpact, 1f);
                        spawnDamageEffect = false;
                        damageAmount = 0;
                    }
                    else if(Blocking == true)
                    {
                        BlockSucceed = true;
                        HeroController.instance.parryInvulnTimer = 0.31f;
                        if(SuperNailCharged == true && this_.playerData.GetBool("equippedCharm_15"))
                        {
                            HeroController.instance.gameObject.GetComponent<HeroBlocking>().Revenge_S();
                            SuperNailCharged = false;
                            PT.GetComponent<ParticleSystem>().enableEmission = false;
                        }
                        else
                        {
                            HeroController.instance.gameObject.GetComponent<HeroBlocking>().Revenge();
                            SuperNailCharged = false;
                            PT.GetComponent<ParticleSystem>().enableEmission = false;
                        }
                        spawnDamageEffect = false;
                        damageAmount = 0;
                    }
                    else
                    {
                        this_.proxyFSM.SendEvent("HeroCtrl-HeroDamaged");
                        NailChargeCount = 0;
                        HeroController.instance.gameObject.GetComponent<NewDashSlash>().Reset();
                        SuperNailCharged = false;
                        PT.GetComponent<ParticleSystem>().enableEmission = false;
                    }
                    this_.CancelAttack();
                    if (this_.cState.wallSliding)
                    {
                        this_.cState.wallSliding = false;
                        this_.wallSlideVibrationPlayer.Stop();
                    }
                    if (this_.cState.touchingWall)
                    {
                        this_.cState.touchingWall = false;
                    }
                    if (this_.cState.recoilingLeft || this_.cState.recoilingRight)
                    {
                        this_.CancelRecoilHorizontal();
                    }
                    if (this_.cState.bouncing)
                    {
                        this_.CancelBounce();
                        this_.rb2d.velocity = new Vector2(this_.rb2d.velocity.x, 0f);
                    }
                    if (this_.cState.shroomBouncing)
                    {
                        this_.CancelBounce();
                        this_.rb2d.velocity = new Vector2(this_.rb2d.velocity.x, 0f);
                    }
                    if (!flag)
                    {
                        this_.audioCtrl.PlaySound(HeroSounds.TAKE_HIT);
                    }
                    if (!this_.takeNoDamage && !this_.playerData.GetBool("invinciTest"))
                    {
                        if (this_.playerData.GetBool("overcharmed"))
                        {
                            this_.playerData.TakeHealth(damageAmount * 2);
                        }
                        else
                        {
                            this_.playerData.TakeHealth(damageAmount);
                        }
                    }
                    if (this_.playerData.GetBool("equippedCharm_3") && damageAmount > 0)
                    {
                        if (this_.playerData.GetBool("equippedCharm_35"))
                        {
                            this_.AddMPCharge(this_.GRUB_SOUL_MP_COMBO);
                        }
                        else
                        {
                            this_.AddMPCharge(this_.GRUB_SOUL_MP);
                        }
                    }
                    if (this_.joniBeam && damageAmount > 0)
                    {
                        this_.joniBeam = false;
                    }
                    if (this_.cState.nailCharging || this_.nailChargeTimer != 0f)
                    {
                        this_.cState.nailCharging = false;
                        this_.nailChargeTimer = 0f;
                    }
                    if (damageAmount > 0)
                    {
                    }
                    if (this_.playerData.GetInt("health") == 0)
                    {
                        self.StartCoroutine(this_.Die());
                        return;
                    }
                    if (hazardType == 2)
                    {
                        self.StartCoroutine(this_.DieFromHazard(HazardType.SPIKES, (!(go != null)) ? 0f : go.transform.rotation.z));
                        return;
                    }
                    if (hazardType == 3)
                    {
                        self.StartCoroutine(this_.DieFromHazard(HazardType.ACID, 0f));
                        return;
                    }
                    if (hazardType == 4)
                    {
                        Debug.Log("Lava death");
                        return;
                    }
                    if (hazardType == 5)
                    {
                        self.StartCoroutine(this_.DieFromHazard(HazardType.PIT, 0f));
                        return;
                    }
                    if(BlockSucceed == false)
                    {
                        self.StartCoroutine(this_.StartRecoil(damageSide, spawnDamageEffect, damageAmount));
                    }
                    else
                    {
                        BlockSucceed = false;
                    }
                    return;
                }
                else if (this_.cState.invulnerable && !this_.cState.hazardDeath && !this_.playerData.GetBool("isInvincible"))
                {
                    if (hazardType == 2)
                    {
                        if (!this_.takeNoDamage)
                        {
                            this_.playerData.TakeHealth(damageAmount);
                        }
                        this_.proxyFSM.SendEvent("HeroCtrl-HeroDamaged");
                        if (this_.playerData.GetInt("health") == 0)
                        {
                            self.StartCoroutine(this_.Die());
                            return;
                        }
                        this_.audioCtrl.PlaySound(HeroSounds.TAKE_HIT);
                        self.StartCoroutine(this_.DieFromHazard(HazardType.SPIKES, (!(go != null)) ? 0f : go.transform.rotation.z));
                        return;
                    }
                    else if (hazardType == 3)
                    {
                        this_.playerData.TakeHealth(damageAmount);
                        this_.proxyFSM.SendEvent("HeroCtrl-HeroDamaged");
                        if (this_.playerData.GetInt("health") == 0)
                        {
                            self.StartCoroutine(this_.Die());
                            return;
                        }
                        self.StartCoroutine(this_.DieFromHazard(HazardType.ACID, 0f));
                        return;
                    }
                    else if (hazardType == 4)
                    {
                        Debug.Log("Lava damage");
                    }
                }
            }
        }

        public void NailSlash_StartSlash(On.NailSlash.orig_StartSlash orig, NailSlash self)
        {
            var this_ = self.Reflect();

            this_.audio.Play();
            if(SlashAngleReverse == true)
            {
                this_.slashFsm.FsmVariables.FindFsmFloat("direction").Value += 180f;
            }
            this_.slashAngle = this_.slashFsm.FsmVariables.FindFsmFloat("direction").Value;
            if (NailType == 0)
            {
                if (this_.mantis && this_.longnail)
                {
                    self.transform.localScale = new Vector3(this_.scale.x * 1.4f, this_.scale.y * 1.4f, this_.scale.z);
                    this_.anim.Play(this_.animName + " M");
                }
                else if (this_.mantis)
                {
                    self.transform.localScale = new Vector3(this_.scale.x * 1.25f, this_.scale.y * 1.25f, this_.scale.z);
                    this_.anim.Play(this_.animName + " M");
                }
                else if (this_.longnail)
                {
                    self.transform.localScale = new Vector3(this_.scale.x * 1.15f, this_.scale.y * 1.15f, this_.scale.z);
                    this_.anim.Play(this_.animName);
                }
                else
                {
                    self.transform.localScale = this_.scale;
                    this_.anim.Play(this_.animName);
                }
            }
            else if (NailType == 1)
            {
                self.transform.localScale = new Vector3(this_.scale.x * 1.7f, this_.scale.y * 1.7f, this_.scale.z);
                this_.anim.Play(this_.animName + " M");
            }
            else if (NailType == 2)
            {
                self.transform.localScale = new Vector3(this_.scale.x * 2.1f, this_.scale.y * 0.9f, this_.scale.z);
                this_.anim.Play(this_.animName + " M");
            }
            else if (NailType == 3)
            {
                self.transform.localScale = new Vector3(this_.scale.x * 2.1f, this_.scale.y * 2.1f, this_.scale.z);
                this_.anim.Play(this_.animName + " M");
            }
            else if (NailType == 4)
            {
                self.transform.localScale = new Vector3(this_.scale.x * 4f, this_.scale.y * 4f, this_.scale.z);
                this_.anim.Play(this_.animName + " F");
            }
            else if (NailType == 5)
            {
                self.transform.localScale = new Vector3(this_.scale.x * 1.65f, this_.scale.y * 1.65f, this_.scale.z);
                this_.anim.Play(this_.animName + " M");
            }
            else if (NailType == 6)
            {
                self.transform.localScale = new Vector3(this_.scale.x * 3f, this_.scale.y * 3f, this_.scale.z);
                this_.anim.Play(this_.animName + " F");
            }
            if (this_.fury)
            {
                this_.anim.Play(this_.animName + " F");
            }
            this_.anim.PlayFromFrame(0);
            this_.stepCounter = 0;
            this_.polyCounter = 0;
            this_.poly.enabled = false;
            this_.clashTinkPoly.enabled = false;
            this_.animCompleted = false;
            this_.anim.AnimationCompleted = new Action<tk2dSpriteAnimator, tk2dSpriteAnimationClip>(this_.Disable);
            this_.slashing = true;
            this_.mesh.enabled = true;
        }


        private void HealthManager_TakeDamage(On.HealthManager.orig_TakeDamage orig, HealthManager self, HitInstance hitInstance)
        {
            var this_ = self.Reflect();
            if (hitInstance.AttackType == AttackTypes.Acid && this_.ignoreAcid)
            {
                return;
            }
            if (CheatManager.IsInstaKillEnabled)
            {
                hitInstance.DamageDealt = 9999;
            }
            int cardinalDirection = DirectionUtils.GetCardinalDirection(hitInstance.GetActualDirection(self.transform));
            this_.directionOfLastAttack = cardinalDirection;
            FSMUtility.SendEventToGameObject(self.gameObject, "HIT", false);
            FSMUtility.SendEventToGameObject(hitInstance.Source, "HIT LANDED", false);
            FSMUtility.SendEventToGameObject(self.gameObject, "TOOK DAMAGE", false);
            if (this_.sendHitTo != null)
            {
                FSMUtility.SendEventToGameObject(this_.sendHitTo, "HIT", false);
            }
            if (this_.recoil != null)
            {
                this_.recoil.RecoilByDirection(cardinalDirection, hitInstance.MagnitudeMultiplier);
            }
            if(HeroController.instance.playerData.GetBool("equippedCharm_33"))
            {
                HeroController.instance.SoulGain();
                switch (hitInstance.AttackType)
                {
                    case AttackTypes.Nail:
                    case AttackTypes.NailBeam:
                        {
                            Vector3 position = (hitInstance.Source.transform.position + self.transform.position) * 0.5f + this_.effectOrigin;
                            this_.strikeNailPrefab.Spawn(position, Quaternion.identity);
                            GameObject gameObject = this_.slashImpactPrefab.Spawn(position, Quaternion.identity);
                            switch (cardinalDirection)
                            {
                                case 0:
                                    gameObject.transform.SetRotation2D((float)UnityEngine.Random.Range(340, 380));
                                    gameObject.transform.localScale = new Vector3(1.5f, 1.5f, 1f);
                                    break;
                                case 1:
                                    gameObject.transform.SetRotation2D((float)UnityEngine.Random.Range(70, 110));
                                    gameObject.transform.localScale = new Vector3(1.5f, 1.5f, 1f);
                                    break;
                                case 2:
                                    gameObject.transform.SetRotation2D((float)UnityEngine.Random.Range(340, 380));
                                    gameObject.transform.localScale = new Vector3(-1.5f, 1.5f, 1f);
                                    break;
                                case 3:
                                    gameObject.transform.SetRotation2D((float)UnityEngine.Random.Range(250, 290));
                                    gameObject.transform.localScale = new Vector3(1.5f, 1.5f, 1f);
                                    break;
                            }
                            break;
                        }
                    case AttackTypes.Generic:
                        this_.strikeNailPrefab.Spawn(self.transform.position + this_.effectOrigin, Quaternion.identity).transform.SetPositionZ(0.0031f);
                        break;
                    case AttackTypes.Spell:
                        this_.fireballHitPrefab.Spawn(self.transform.position + this_.effectOrigin, Quaternion.identity).transform.SetPositionZ(0.0031f);
                        break;
                    case AttackTypes.SharpShadow:
                        this_.sharpShadowImpactPrefab.Spawn(self.transform.position + this_.effectOrigin, Quaternion.identity);
                        break;
                }
            }
            else
            {
                switch (hitInstance.AttackType)
                {
                    case AttackTypes.Nail:
                    case AttackTypes.NailBeam:
                        {
                            if (hitInstance.AttackType == AttackTypes.Nail && this_.enemyType != 3 && this_.enemyType != 6)
                            {
                                if (hitInstance.Source.name.Contains("Clone"))
                                {

                                }
                                else
                                {
                                    HeroController.instance.SoulGain();
                                }
                            }
                            Vector3 position = (hitInstance.Source.transform.position + self.transform.position) * 0.5f + this_.effectOrigin;
                            this_.strikeNailPrefab.Spawn(position, Quaternion.identity);
                            GameObject gameObject = this_.slashImpactPrefab.Spawn(position, Quaternion.identity);
                            switch (cardinalDirection)
                            {
                                case 0:
                                    gameObject.transform.SetRotation2D((float)UnityEngine.Random.Range(340, 380));
                                    gameObject.transform.localScale = new Vector3(1.5f, 1.5f, 1f);
                                    break;
                                case 1:
                                    gameObject.transform.SetRotation2D((float)UnityEngine.Random.Range(70, 110));
                                    gameObject.transform.localScale = new Vector3(1.5f, 1.5f, 1f);
                                    break;
                                case 2:
                                    gameObject.transform.SetRotation2D((float)UnityEngine.Random.Range(340, 380));
                                    gameObject.transform.localScale = new Vector3(-1.5f, 1.5f, 1f);
                                    break;
                                case 3:
                                    gameObject.transform.SetRotation2D((float)UnityEngine.Random.Range(250, 290));
                                    gameObject.transform.localScale = new Vector3(1.5f, 1.5f, 1f);
                                    break;
                            }
                            break;
                        }
                    case AttackTypes.Generic:
                        this_.strikeNailPrefab.Spawn(self.transform.position + this_.effectOrigin, Quaternion.identity).transform.SetPositionZ(0.0031f);
                        break;
                    case AttackTypes.Spell:
                        this_.fireballHitPrefab.Spawn(self.transform.position + this_.effectOrigin, Quaternion.identity).transform.SetPositionZ(0.0031f);
                        break;
                    case AttackTypes.SharpShadow:
                        this_.sharpShadowImpactPrefab.Spawn(self.transform.position + this_.effectOrigin, Quaternion.identity);
                        break;
                }
            }    
            if (this_.hitEffectReceiver != null && hitInstance.AttackType != AttackTypes.RuinsWater)
            {
                this_.hitEffectReceiver.RecieveHitEffect(hitInstance.GetActualDirection(self.transform));
            }
            int num = Mathf.RoundToInt((float)hitInstance.DamageDealt * hitInstance.Multiplier);
            if (this_.damageOverride)
            {
                num = 1;
            }
            this_.hp = Mathf.Max(this_.hp - num, -50);
            if (this_.hp > 0)
            {
                this_.NonFatalHit(hitInstance.IgnoreInvulnerable);
                if (this_.stunControlFSM)
                {
                    this_.stunControlFSM.SendEvent("STUN DAMAGE");
                    return;
                }
            }
            else
            {
                this_.Die(new float?(hitInstance.GetActualDirection(self.transform)), hitInstance.AttackType, hitInstance.IgnoreInvulnerable);
            }
        }

        private void PlayMakerFSM_Start(On.PlayMakerFSM.orig_Start orig, PlayMakerFSM self)
        {
            orig(self);
            if(self.gameObject.name == "Knight" && self.FsmName == "Nail Arts")
            {
                self.gameObject.AddComponent<Cleaner>();
                self.gameObject.AddComponent<NewGreatSlash>();
                self.gameObject.AddComponent<NewDashSlash>();
                self.gameObject.AddComponent<NewCycloneSlash>();
                self.gameObject.AddComponent<HeroBlocking>();
                self.gameObject.AddComponent<SuperNailCharge>();
                self.gameObject.AddComponent<UpDashChangeScale>();
                self.gameObject.AddComponent<BlockingWavesSummon>();
                self.gameObject.AddComponent<SuperNailChargePt>();
                self.gameObject.AddComponent<SuperSpell>();
                self.gameObject.AddComponent<MPCharge>();
                var attacks = self.gameObject.transform.Find("Attacks").gameObject;
                attacks.transform.Find("Great Slash").gameObject.Recycle();
                attacks.transform.Find("Sharp Shadow").gameObject.AddComponent<DashBlastSummon>();
                self.GetState("G Slash").RemoveAction<AudioPlay>();
                self.GetState("G Slash").AddMethod(()=>
                {
                    NAILART = self.GetState("Dash Slash").GetAction<AudioPlay>().oneShotClip.Value as AudioClip;
                });
                self.GetState("G Slash").AddMethod(()=>
                {
                    if(SuperNailCharged && HeroController.instance.playerData.GetBool("equippedCharm_15"))
                    {
                        self.gameObject.GetComponent<HeroBlocking>().BlockFlash_S_Try();
                        self.gameObject.GetComponent<HeroBlocking>().S_Ready_ConsumeLately();
                        HeroController.instance.CancelParryInvuln();
                        self.gameObject.GetComponent<AudioSource>().PlayOneShot(BLOCKING, 1.5f);
                        self.gameObject.GetComponent<HeroBlocking>().BlockLongTime();
                    }
                    else
                    {
                        self.gameObject.GetComponent<HeroBlocking>().BlockFlash();
                        HeroController.instance.CancelParryInvuln();
                        self.gameObject.GetComponent<AudioSource>().PlayOneShot(BLOCKING, 1.3f);
                        if (HeroController.instance.playerData.GetBool("equippedCharm_15"))
                        {
                            self.gameObject.GetComponent<HeroBlocking>().BlockLongTime();
                        }
                        else
                        {
                            self.gameObject.GetComponent<HeroBlocking>().BlockShortTime();
                        }
                    }
                    //self.gameObject.GetComponent<NewGreatSlash>().DoSlash();
                });
                attacks.transform.Find("Dash Slash").gameObject.Recycle();
                self.GetState("Dash Slash").AddMethod(()=>
                {
                    if(SuperNailCharged && HeroController.instance.playerData.GetBool("equippedCharm_31"))
                    {
                        self.gameObject.GetComponent<NewDashSlash>().FirstContinueDashTimer_Start_S();
                        SuperNailCharged = false;
                        PT.GetComponent<ParticleSystem>().enableEmission = false;
                    }
                    else
                    {
                        self.gameObject.GetComponent<NewDashSlash>().FirstContinueDashTimer_Start();
                    }
                });
                attacks.transform.Find("Cyclone Slash").gameObject.Recycle();
                self.GetState("Cyclone Spin").AddMethod(()=>
                {
                    if (SuperNailCharged && HeroController.instance.playerData.GetBool("equippedCharm_16"))
                    {
                        self.gameObject.GetComponent<NewCycloneSlash>().Slash_S();
                    }
                    else
                    {
                        self.gameObject.GetComponent<NewCycloneSlash>().Slash();
                    }
                });
                /*
                attacks.transform.Find("Cyclone Slash").gameObject.Recycle();
                self.GetState("Cyclone Spin").AddMethod(()=>
                {
                    self.gameObject.GetComponent<NewCycloneSlash>().DoSlash();
                });
                */
            }
            if(self.gameObject.name == "Knight" && self.FsmName == "Spell Control")
            {
                var spells = self.gameObject.transform.Find("Spells").gameObject;
                var scr = spells.transform.Find("Scr Heads 2");
                var qorb = spells.transform.Find("Q Orbs");
                var qorb2 = spells.transform.Find("Q Orbs 2");
                var sorb = spells.transform.Find("Scr Orbs");
                var sorb2 = spells.transform.Find("Scr Orbs 2");
                var qsl = spells.transform.Find("Q Slam 2");
                var qmag = spells.transform.Find("Q Mega");
                var qpl = spells.transform.Find("Q Pillar");
                scr.Recycle();
                qorb.Recycle();
                qorb2.Recycle();
                sorb.Recycle();
                sorb2.Recycle();
                qsl.Recycle();
                qmag.Recycle();
                qpl.Recycle();
                self.gameObject.AddComponent<SceneSwitchDetector>();
                self.gameObject.AddComponent<NailSkill1>();
                self.gameObject.AddComponent<NailSkill2>();
                self.gameObject.AddComponent<BlastSkill1>();
                self.gameObject.AddComponent<BlastSkill2>();
                self.gameObject.AddComponent<PlumeSkill1>();
                self.gameObject.AddComponent<PlumeSkill2>();
                self.gameObject.AddComponent<FocusUp>();
                self.CopyState("Spell End", "SuperCast");
                self.GetState("SuperCast").AddMethod(()=>
                {
                    if(GameManager.instance.GetPlayerDataInt("MPCharge") >= self.FsmVariables.GetFsmInt("MP Cost").Value)
                    {
                        if (GameManager.instance.inputHandler.inputActions.quickCast.IsPressed && !GameManager.instance.inputHandler.inputActions.up.IsPressed && GameManager.instance.inputHandler.inputActions.down.IsPressed)
                        {
                            self.GetComponent<SuperSpell>().PlumeEnhance();
                            self.SetState("Has Quake?");
                        }
                        else if (GameManager.instance.inputHandler.inputActions.quickCast.IsPressed && GameManager.instance.inputHandler.inputActions.up.IsPressed && !GameManager.instance.inputHandler.inputActions.down.IsPressed)
                        {
                            self.GetComponent<SuperSpell>().BlastEnhance();
                            self.SetState("Has Scream?");
                        }
                        else if (GameManager.instance.inputHandler.inputActions.quickCast.IsPressed)
                        {
                            self.GetComponent<SuperSpell>().NailEnhance();
                            self.SetState("Has Fireball?");
                        }
                    }
                });
                //self.ChangeTransition("Fireball Recoil", "ANIM END", "SuperCast");
                //self.ChangeTransition("Quake Finish", "FINISHED", "SuperCast");
                //self.ChangeTransition("Sream End 2", "FINISHED", "SuperCast");
                self.ChangeTransition("Spell End", "FINISHED", "SuperCast");
                self.ChangeTransition("SuperCast", "FINISHED", "Inactive");
                self.GetState("Fireball Antic").AddMethod(() =>
                {
                    if(HeroController.instance.playerData.GetBool("equippedCharm_19"))
                    {
                        self.gameObject.GetComponent<NailSkill2>().Fire();
                    }
                    else
                    {
                        if (self.gameObject.transform.localScale.x <= 0)
                        {
                            self.gameObject.GetComponent<NailSkill1>().R();
                        }
                        else
                        {
                            self.gameObject.GetComponent<NailSkill1>().L();
                        }
                    }
                });
                self.GetState("Fireball 2").RemoveAction<SpawnObjectFromGlobalPool>();
                self.GetState("Fireball 1").RemoveAction<SpawnObjectFromGlobalPool>();

                self.GetState("Scream Burst 2").AddMethod(() =>
                {
                    if (HeroController.instance.playerData.GetBool("equippedCharm_19"))
                    {
                        self.gameObject.GetComponent<BlastSkill2>().Blast();
                    }
                    else
                    {
                        self.gameObject.GetComponent<BlastSkill1>().Blast();
                    }
                });
                self.GetState("Quake Antic").AddMethod(() =>
                {
                    if (HeroController.instance.playerData.GetBool("equippedCharm_19") && CanSuperQuake)
                    {
                        self.gameObject.GetComponent<PlumeSkill2>().Freeze();
                    }
                });
                self.GetState("Q2 Land").RemoveAction<AudioPlay>();
                self.GetState("Q2 Land").AddMethod(() =>
                {
                    if (HeroController.instance.playerData.GetBool("equippedCharm_19"))
                    {
                        self.gameObject.GetComponent<PlumeSkill2>().Quake();
                    }
                    else
                    {
                        self.gameObject.GetComponent<PlumeSkill1>().Land();
                    }
                });
                self.GetState("Quake2 Down").GetAction<SetVelocity2d>().y.Value = -500f;
                self.GetState("Focus Start").AddMethod(()=>
                {
                });
                self.GetState("Regain Control").AddMethod(() =>
                {
                });
                self.GetState("Focus").AddMethod(() =>
                {
                    if (HeroController.instance.playerData.GetBool("equippedCharm_7") && !HeroController.instance.playerData.GetBool("equippedCharm_34"))
                    {
                        self.gameObject.GetComponent<FocusUp>().Focus(0.75f, 1.8f);
                    }
                    else if(!HeroController.instance.playerData.GetBool("equippedCharm_7") && HeroController.instance.playerData.GetBool("equippedCharm_34"))
                    {
                        self.gameObject.GetComponent<FocusUp>().Focus(1f, 0.45f);
                    }
                    else if(HeroController.instance.playerData.GetBool("equippedCharm_7") && HeroController.instance.playerData.GetBool("equippedCharm_34"))
                    {
                        self.gameObject.GetComponent<FocusUp>().Focus(1f, 0.9f);
                    }
                    else
                    {
                        self.gameObject.GetComponent<FocusUp>().Focus(0.75f, 1.1f);
                    }

                    self.GetComponent<AudioSource>().PlayOneShot(BALLUP, 1f);
                });
                self.GetState("Start MP Drain").AddMethod(() =>
                {
                    if (HeroController.instance.playerData.GetBool("equippedCharm_7") && !HeroController.instance.playerData.GetBool("equippedCharm_34"))
                    {
                        self.gameObject.GetComponent<FocusUp>().Focus(0.75f, 1.8f);
                    }
                    else if(!HeroController.instance.playerData.GetBool("equippedCharm_7") && HeroController.instance.playerData.GetBool("equippedCharm_34"))
                    {
                        self.gameObject.GetComponent<FocusUp>().Focus(1f, 0.45f);
                    }
                    else if(HeroController.instance.playerData.GetBool("equippedCharm_7") && HeroController.instance.playerData.GetBool("equippedCharm_34"))
                    {
                        self.gameObject.GetComponent<FocusUp>().Focus(1f, 0.9f);
                    }
                    else
                    {
                        self.gameObject.GetComponent<FocusUp>().Focus(0.75f, 1.1f);
                    }

                    self.GetComponent<AudioSource>().PlayOneShot(BALLUP, 1f);
                });
                self.GetState("Focus Heal").AddMethod(() =>
                {
                    if (HeroController.instance.playerData.GetBool("equippedCharm_34"))
                    {
                        self.gameObject.GetComponent<FocusUp>().FocusBlast(1.1f, 100);
                    }
                    else
                    {
                        self.gameObject.GetComponent<FocusUp>().FocusBlast(0.8f, 75);
                    }
                    self.GetComponent<AudioSource>().PlayOneShot(BALLEXPLODE, 1f);
                });
                self.GetState("Focus Heal 2").AddMethod(() =>
                {
                    if (HeroController.instance.playerData.GetBool("equippedCharm_34"))
                    {
                        self.gameObject.GetComponent<FocusUp>().FocusBlast(1.1f, 100);
                    }
                    else
                    {
                        self.gameObject.GetComponent<FocusUp>().FocusBlast(0.8f, 75);
                    }
                    self.GetComponent<AudioSource>().PlayOneShot(BALLEXPLODE, 1f);
                });
                self.GetState("Focus Cancel").AddMethod(() =>
                {
                    self.gameObject.GetComponent<FocusUp>().FocusCancel();
                });
                self.GetState("Focus Cancel 2").AddMethod(() =>
                {
                    self.gameObject.GetComponent<FocusUp>().FocusCancel();
                });
                self.GetState("Full HP?").RemoveAction(0);
                self.GetState("Full HP? 2").RemoveAction(0);
            }
        }
    }
    public class Settings
    {
        public bool on = true;
    }
}
