mergeInto(LibraryManager.library, {
    OpenURL: function (score) { 
        score = UTF8ToString(score);  
        var gameCanvas = document.getElementById("UT_CANVAS");  

        var scheme = "twitter://post?message=" + score + "個のニンジンを集めました\nhttps://poikelp.github.io/TinyPeko @poi_third より";
        var pc_site = "https://twitter.com/intent/tweet?text=" + score + "個のニンジンを集めました\n&via=poi_third&url=https://poikelp.github.io/TinyPeko";

        var userAgent = navigator.userAgent.toLowerCase();
        if (gameCanvas != null)  {
            
            if(userAgent.indexOf("android") !== -1 
                || userAgent.indexOf("iphone") !== -1
                || userAgent.indexOf("ipad") !== -1)
            {
                if(window.open(scheme, "_blank"))
                {}
                else
                {
                    window.location.href = scheme;
                    setTimeout(function() 
                    {
                        window.location.href = pc_site;
                    }, 500);
                }
            }
            else
            {
                if(window.open(pc_site, "_blank"))
                {}
                else
                {
                    window.location.href = pc_site;
                }
            }

            
            
        } else {
            console.error("UT_CANVAS not found, was it renamed?");
        }
    }
});