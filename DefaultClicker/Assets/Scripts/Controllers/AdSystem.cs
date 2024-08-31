using SekiburaGames.DefaultClicker.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using YG;

namespace Assets.Scripts
{
    internal class AdSystem : IInitializable
    {
        private int _adDelay;

        public void Initialize()
        {
            
        }

        public void FullScreenAd()
        {

#if YG_BUILD
            YandexGame.FullscreenShow();
#endif

#if RUSTORE_BUILD

#endif
        }
    }
}
