
namespace PlantEng.Core
{
    public class PlantEngSites 
    {
        private bool _isDemo = false;
        public PlantEngSites() { 
            if(System.Web.HttpContext.Current.Request.Url.Host.ToLower().IndexOf(".3721.com")>0){
                _isDemo = true;
            }
        }
        public static PlantEngSites Current
        {
            get { return new PlantEngSites(); }
        }
        public string WWW {
            get { 
                if(_isDemo){
                    return "http://www.planteng.cn.3721.com";
                }
                return "http://www.planteng.cn";
            }
        }
        public string Image {
            get
            {
                if (_isDemo)
                {
                    return "http://images.planteng.cn.3721.com";
                }
                return "http://images.planteng.cn";
            } 
        }
    }
}
