using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Class
{
    public static class NextPage
    {
        private static ProductPage productPage;
        private static ActionPage actionPage;
        private static ActionPage2 actionPage2;


        public static ProductPage GetProductPage()
        {
            if (productPage == null)
            {
                productPage = new ProductPage();
            }
            return productPage;
        }
        public static ActionPage GetActionPage()
        {
            if (actionPage == null)
            {
                actionPage = new ActionPage();
            }
            return actionPage;
        }

        public static ActionPage2 GetActionPage2()
        {
            if (actionPage2 == null)
            {
                actionPage2 = new ActionPage2();
            }
            return actionPage2;
        }
    }

    
}
