
(function () {
    //GLOBALS
    //globals for classMausWork
    var gSelectedElement;	//currently only one selection
    var gHoverElement;		//whatever element the mouse is over
    var gHovering = false;	//mouse is over something
    var gObjArrMW = [];	//global array of classMausWork objects.  for removing event listeners when done selecting.

    //extended	
    var infoDiv;		//currently just container for InfoDivHover, might add more here
    var infoDivHover;	//container for hoverText text node.
    var hoverText;		//show information about current element that the mouse is over
    //const EXPERIMENTAL_NEW_CODE=true;	//debugging. new features.


    //START
    SetupDOMSelection();

    //(Section 1) Element Selection
    function SetupDOMSelection() {
        {
            //setup event listeners
            //var pathx="//div | //span | //table | //td | //tr | //ul | //ol | //li | //p";
            var pathx = "//div | //span | //a | //strong | //img | //table | //th | //td | //tr | //ul | //ol | //li | //p | //h1 | //h2";
            var selection = $XPathSelect(pathx);
            for (var element, i = 0; element = selection(i); i++) {
                if (element.tagName.match(/^(div|span|table|td|tr|ul|ol|li|p|strong|a|img|h1|h2)$/i))	//redundant check.
                {
                    var m = new classMausWork(element);
                    gObjArrMW.push(m);
                    attachMouseEventListeners(m);
                }
            }
            document.body.addEventListener('mousedown', MiscEvent, false);
            document.body.addEventListener('mouseover', MiscEvent, false);
            document.body.addEventListener('mouseout', MiscEvent, false);
            document.addEventListener('keypress', MiscEvent, false);
        }
        {
            //setup informational div to show which element the mouse is over.
            infoDiv = document.createElement('div');
            var s = infoDiv.style;
            s.position = 'fixed';
            s.top = '0';
            s.right = '0';

            s.display = 'block';
            s.width = 'auto';
            s.padding = '0px';
            document.body.appendChild(infoDiv);
            infoDivHover = document.createElement('div');
            s = infoDivHover.style;
            s.fontWeight = 'bold';
            s.padding = '3px';
            s.Opacity = '0.8';
            s.borderWidth = 'thin';
            s.borderStyle = 'solid';
            s.borderColor = 'white';
            s.backgroundColor = 'black';
            s.color = 'white';

            infoDiv.appendChild(infoDivHover);
            hoverText = document.createTextNode('selecting');
            infoDivHover.appendChild(hoverText);
        }
    }

    function CleanupDOMSelection() {
        for (var m; m = gObjArrMW.pop();) {
            detachMouseEventListeners(m);
        }
        ElementRemove(infoDiv);
        document.body.removeEventListener('mousedown', MiscEvent, false);
        document.body.removeEventListener('mouseover', MiscEvent, false);
        document.body.removeEventListener('mouseout', MiscEvent, false);
        document.removeEventListener('keypress', MiscEvent, false);
    }
    function attachMouseEventListeners(c) {
        //c is object of class classMausWork
        c.element.addEventListener("mouseover", c.mouse_over, false);
        c.element.addEventListener("mouseout", c.mouse_out, false);
        c.element.addEventListener("mousedown", c.mouse_click, false);
    }
    function detachMouseEventListeners(c) {
        //c is object of class classMausWork
        c.resetElementStyle();
        c.element.removeEventListener("mouseover", c.mouse_over, false);
        c.element.removeEventListener("mouseout", c.mouse_out, false);
        c.element.removeEventListener("mousedown", c.mouse_click, false);
    }
    //mouse event  handling class for element, el.
    function classMausWork(element) {
        //store information about the element this object is assigned to handle. element,  original style, etc.	
        this.element = element;
        var defaultbgc = element.style.backgroundColor;
        var defaultStyle = element.getAttribute('style');

        this.mouse_over = function (ev) {
            //if (gHovering) return;
            //if (ev.target != element) return;
            var e = element;		//var e=ev.target;
            var s = e.style;
            s.backgroundColor = 'yellow';
            s.borderWidth = 'thin';
            s.borderColor = 'lime';
            s.borderStyle = 'solid';
            InfoMSG(ElementInfo(e), 'yellow', 'blue', 'yellow');
            //gHoverElement = e;		//gHoverElement=ev.target;	//gHoverElement=e;	
            //gHovering = true;
            ev.stopPropagation();
        };
        this.mouse_out = function (ev) {
            //if (ev.target != gHoverElement) return;
            var e = element;		//var e=ev.target;
            e.setAttribute('style', defaultStyle);	// ev.target.setAttribute('style',defaultStyle);			
            InfoMSG('-', 'white', 'black', 'white');
            //gHoverElement = null;
            //gHovering = false;
            //ev.stopPropagation();
        };
        this.mouse_click = function (ev) {
           
           
            //if (ev.target != gHoverElement) return;
            var e = element;		//var e=ev.target;
            e.setAttribute('style', defaultStyle);  //ev.target.setAttribute('style',defaultStyle);		
            gSelectedElement = e;		//gSelectedElement=ev.target;		//=ev.target;
            ev.stopPropagation();

            if (ev.buttons === 1) {
                //ev.preventDefault();
                //SetupDOMSelection();
                return;
            }
            			
           
            //CleanupDOMSelection();
            //gHoverElement = null;
            //gHovering = false;
         
            ElementSelected(gSelectedElement);	//finished selecting, cleanup then move to next part, element isolation.
        };
        this.resetElementStyle = function () {
            if (gHovering && gHoverElement && gHoverElement == this.element) {
                this.element.setAttribute('style', defaultStyle);
            }
        };
    }
    function MiscEvent(ev)		//keypress, and mouseover/mouseout/mousedown event on body.  cancel selecting.
    {
        if (ev.type == 'mouseout') {
            InfoMSG('-', 'white', 'black', 'white');
        }
        else if (ev.type == 'mouseover') {
            InfoMSG('cancel', 'yellow', 'red', 'yellow');
        }
        else //keypress on document or mousedown on body, cancel ops.
        {
            CleanupDOMSelection();
        }
    }

    function InfoMSG(text, color, bgcolor, border) {

        var s = infoDivHover.style;
        //var s=infoDiv.style;		
        if (color) s.color = color;
        if (bgcolor) s.backgroundColor = bgcolor;
        if (border) s.borderColor = border;
        if (text) hoverText.data = text;
    }



    //(Section 2) Element Isolation
    function ElementSelected(element)	//finished selecting element.  setup string to prompt user.
    {
        PromptUserXpath(ElementInfo(element), element.innerText);
    }
    function CreateGuid() {
        function _p8(s) {
            var p = (Math.random().toString(16) + "000000000").substr(2, 8);
            return s ? "-" + p.substr(0, 4) + "-" + p.substr(4, 4) : p;
        }
        return _p8() + _p8(true) + _p8(true) + _p8();
    }  

    function PromptUserXpath(defaultpath,innertext)		//prompt user, isolate element.
    {
        var id = CreateGuid();
        var data = jQuery.parseJSON(jsondatas.replace(/&quot;/g, '"'));
        var cmbstr = "<select style='width:200px' name='cmb" + id +"' id='cmb" + id+"'>";
        $(data).each(function(i,v) {
            cmbstr = cmbstr + "<option>"+v+"</option>";
        });
        cmbstr = cmbstr + "</select>";
        cmbstr = cmbstr +
            '<input style="width: 200px" name="chck' +
            id +
            '" id="chck' +
            id +
            '" type="text" value="' +
            defaultpath +
            '"/><label>' +
            innertext +
            '</label><br/>';
        var userpath = prompt("XPath of elements to isolate : ", defaultpath);
        $("#xpath-list").append("<div>" + cmbstr+"</div>");
    }
   

    function $XPathSelect(p, context) {
        if (!context) context = document;
        var xpr = document.evaluate(p, context, null, XPathResult.UNORDERED_NODE_SNAPSHOT_TYPE, null);
        return function (x) { return xpr.snapshotItem(x); };
    }
    function ElementRemove(e) {
        e.parentNode.removeChild(e);
    }

    function ElementInfo(element) {
        return getNodeXPath(element);
    }


    /**
 * Gets an XPath for an node which describes its hierarchical location.
 */
    var getNodeXPath = function (node) {
        if (node && node.id)
            return "//*[@id='" + node.id + "']";
        else
            return getNodeTreeXPath(node);
    };

    var getNodeTreeXPath = function (node) {
        var paths = [];

        // Use nodeName (instead of localName) so namespace prefix is included (if any).
        for (; node && (node.nodeType == 1 || node.nodeType == 3); node = node.parentNode) {
            var index = 0;
            // EXTRA TEST FOR ELEMENT.ID
            if (node && node.id) {
                paths.splice(0, 0, "/*[@id='" + node.id + "']");
                break;
            }

            for (var sibling = node.previousSibling; sibling; sibling = sibling.previousSibling) {
                // Ignore document type declaration.
                if (sibling.nodeType == Node.DOCUMENT_TYPE_NODE)
                    continue;

                if (sibling.nodeName == node.nodeName)
                    ++index;
            }

            var tagName = (node.nodeType == 1 ? node.nodeName.toLowerCase() : "text()");
            var pathIndex = (index ? "[" + (index + 1) + "]" : "");
            paths.splice(0, 0, tagName + pathIndex);
        }

        return paths.length ? "/" + paths.join("/") : null;
    };

})();

$(function () {

    $(this).on("contextmenu", function (event) {

        event.preventDefault(); // Sağ menünün default event'ını kapatır.
    });
}); 
