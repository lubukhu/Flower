// using UnityEngine;
// using System.Collections.Generic;

// namespace finished3
// {
//     public class GameController : MonoBehaviour
//     {
//         public static GameController Instance;

//         public GameState CurrentState { get; private set; }

//         private CharacterInfo currentCharacter;
//         private CharacterStats currentStats;

//         private MovementController movementController;
//         private RangeSystem rangeSystem;
//         private AttackController attackController;
//         private MovementSystem movementSystem;

//         private List<OverlayTile> moveTiles;
//         private List<OverlayTile> attackTiles;
//         private List<OverlayTile> path;

//         private JumpMover jumpMover;
//         private ClimbMover climbMover;

//         private void Awake()
//         {
//             Instance = this;

//             movementController = new MovementController();
//             rangeSystem = new RangeSystem();
//             attackController = new AttackController();
//             movementSystem = new MovementSystem();

//             path = new List<OverlayTile>();
//         }

//         public void SetCharacter(CharacterInfo character)
//         {
//             currentCharacter = character;
//             currentStats = character.GetComponent<CharacterStats>();
//             jumpMover = character.GetComponent<JumpMover>();
//             climbMover = character.GetComponent<ClimbMover>();

//             ShowRange();
//         }

//         public void ShowRange()
//         {
//             moveTiles = rangeSystem.GetMoveRange(currentCharacter, currentStats.moveRange);
//             attackTiles = rangeSystem.GetAttackRange(currentCharacter, currentStats.attackRange);

//             TileHighlighter highlighter = new TileHighlighter();
//             highlighter.ShowMoveRange(moveTiles);
//             highlighter.ShowAttackRange(attackTiles);

//             CurrentState = GameState.ShowingRange;
//         }

//         public void TryMove(OverlayTile tile)
//         {
//             if (CurrentState != GameState.ShowingRange) return;
//             if (!moveTiles.Contains(tile)) return;

//             path = movementController.GetPath(currentCharacter, tile, moveTiles);

//             CurrentState = GameState.Moving;

//             MoveStep();
//         }

//         private void MoveStep()
//         {
//             if (path.Count == 0)
//             {
//                 ShowRange();
//                 return;
//             }

//             movementController.MoveAlongPath(
//                 currentCharacter,
//                 jumpMover,
//                 climbMover,
//                 movementSystem,
//                 path,
//                 () =>
//                 {
//                     if (path.Count == 0)
//                     {
//                         ShowRange();
//                     }
//                 }
//             );
//         }

//         public void TryAttack(OverlayTile tile)
//         {
//             if (CurrentState != GameState.ShowingRange) return;

//             if (!attackTiles.Contains(tile)) return;

//             CurrentState = GameState.Attacking;

//             attackController.TryAttack(tile, currentStats);

//             CurrentState = GameState.Busy;

//             // delay nhỏ tránh spam
//             Invoke(nameof(ShowRange), 0.2f);
//         }
//     }
// }