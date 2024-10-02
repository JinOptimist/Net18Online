using MazeCore.Models.Cells.Character;
using MazeCore.Models.Cells;
using MazeCore.Models.Enum;

namespace MazeCore.Actions
{
    public class WanderingMerchantActions
    {
        private readonly WanderingMerchant _merchant;

        public WanderingMerchantActions(WanderingMerchant merchant)
        {
            _merchant = merchant;
        }

        public WanderingMerchantActionResult PerformAction(BaseCharacter character, WanderingMerchantOptions option)
        {
            switch (option)
            {
                case WanderingMerchantOptions.BuyHealingSalve:
                    return TryBuyHealingSalve(character);
                case WanderingMerchantOptions.Exit:
                    return WanderingMerchantActionResult.Exit;
                default:
                    return WanderingMerchantActionResult.Exit;
            }
        }

        private WanderingMerchantActionResult TryBuyHealingSalve(BaseCharacter character)
        {
            if (character.Coins >= 5)
            {
                character.Coins -= 5;
                character.Health += 5;
                return WanderingMerchantActionResult.Success;
            }
            else
            {
                return WanderingMerchantActionResult.InsufficientFunds;
            }
        }
    }
}
