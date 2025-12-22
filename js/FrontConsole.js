//--Imqutive Front Console System--//
//--Version 2025.12.20--//
//--Copyright (c) Torisima 2025--//
//--Licensed under MIT License--//
//
//--If you use without editting, you should use "<script src="https://res.tools.imqutive.f5.si/source/FrontConsole.js"></script>".--//
//--If not, you can change this under MIT License.--//
//
//--Position of this code block should be, at last if you do not have to use your style, at first if you have to use your style--//
//--Mostly, you have to set only back system codes.--//

//Output element
var CUI_outputer = document.createElement("textarea");
CUI_outputer.id = "outputer";
document.body.appendChild(CUI_outputer);

//Style Setting (Change in your code)
document.documentElement.style.colorScheme = 'light dark';
document.documentElement.style.setProperty('--foreground-color', 'light-dark(black, white)');
document.documentElement.style.setProperty('--background-color', 'light-dark(white, black)');
document.documentElement.style.setProperty('--font-size', '16px');

////Style Setting (Change in your code)
document.body.style.overflowX = 'hidden';document.body.style.overflowY = 'hidden';document.body.style.margin = '0';document.body.style.padding = '0';document.body.style.backgroundColor = 'var(--background-color)';document.body.style.color = 'var(--foreground-color)';document.body.style.fontSize = 'var(--font-size)';
CUI_outputer.style.overflowX = 'hidden';CUI_outputer.style.overflowY = 'auto';CUI_outputer.style.whiteSpace = 'pre-wrap';CUI_outputer.style.position = 'absolute';CUI_outputer.style.width = '100vw';CUI_outputer.style.height = '100vh';CUI_outputer.style.top = '0';CUI_outputer.style.left = '0';CUI_outputer.style.border = '0px solid transparent';CUI_outputer.style.textWrap = 'wrap auto';CUI_outputer.style.margin = '0';CUI_outputer.style.padding = '25px';CUI_outputer.style.outline = '0';CUI_outputer.style.backgroundColor = 'var(--background-color)';

//Internal Var
var CUI_ining = -1;

//Write CUI output Function (text : str | out text)
function write_CUI(text) 
{
    CUI_outputer.value += text;
}

//Write CUI output by delay Function (text : str | out text, time : num | delay time)
async function write_CUI_D(text, time) 
{
    return new Promise((resolve)=>
    {
        let CUI_INDEX = 0;
    let CUI_THREAD = setInterval(()=>
    {
        CUI_outputer.value += text[CUI_INDEX];
        CUI_INDEX++;
        if (CUI_INDEX == text.length)
    {
        clearInterval(CUI_THREAD);
        resolve();
    }
    }, time);
    });
}

//Read CUI input Function (nw : bool | do not inputs write? |param - false) [return : str | in text]
async function read_CUI(nw=false) 
{
    return new Promise(resolve=>
    {
    CUI_outputer.addEventListener("keydown", function CUI_hundler(e)
    {
        if (e.key == 'Enter')
    {
        CUI_outputer.removeEventListener("keydown", CUI_hundler);
        let CUI_RETURN = CUI_outputer.value.substr(CUI_ining, CUI_outputer.value.length - CUI_ining);
        e.preventDefault();

        if (nw) CUI_outputer.value = CUI_outputer.value.substr(0, CUI_ining);
        CUI_ining = -1;

        resolve(CUI_RETURN);
    }
    });
    CUI_ining = CUI_outputer.value.length;
    CUI_outputer.selectionStart = CUI_ining;
        CUI_outputer.selectionEnd = CUI_ining;
        CUI_Caret();
        CUI_outputer.focus();
    });
}

//Clear CUI output Function
async function clear_CUI()
{
    CUI_outputer.value = "";
    CUI_ining = -1;
    CUI_Caret();
    CUI_outputer.focus();
}

//Wait CUI update Function (you must not use, in most cases)
async function flush_CUI()
{
    await Promise.resolve();
    void CUI_outputer.offsetHeight;
    await new Promise(r => requestAnimationFrame(r));
    await new Promise(r => requestAnimationFrame(r));
}

//Scroll to bottom Function
function scroll_CUI() {CUI_outputer.scrollTop = CUI_outputer.scrollHeight;}


//Internal Function
function CUI_Caret()
{
    if (CUI_ining == -1) CUI_outputer.style.caretColor = 'transparent';
    else 
    {
        if (CUI_outputer.selectionStart >= CUI_ining) CUI_outputer.style.caretColor = 'var(--foreground-color)';
        else CUI_outputer.style.caretColor = 'transparent';
    }
}

//Internal Function
function CUI_Input(e)
{
    if (CUI_ining == -1)
{
    if (e.key.length == 1)e.preventDefault();
}
else 
{
    if (e.key == 'backspace')
{
    if (CUI_outputer.value.length == CUI_ining) e.preventDefault();
}
}
}

//Simple Delay Function (time : num | delay time)
async function delay_CUI(time)
{
    return new Promise((resolve)=>
    {
        setTimeout(()=>{resolve();}, time);
    });
}

//Internal Events
CUI_outputer.addEventListener("click", CUI_Caret);
CUI_outputer.addEventListener("keyup", CUI_Caret);
CUI_outputer.addEventListener("keydown", CUI_Input);
