{\rtf1\ansi\ansicpg1251\deff0\nouicompat{\fonttbl{\f0\fnil\fcharset204 Cambria;}{\f1\fnil\fcharset0 Cambria;}}
{\*\generator Riched20 10.0.19041}\viewkind4\uc1 
\pard\f0\fs29\lang1049 .286\par
.MODEL SMALL\par
.DATA\par
    A BYTE  15\par
    B BYTE  7\par
.CODE\par
    Main proc\par
\f1\lang1033         XOR al,al\par
        XOR bl,bl\f0\lang1049\par
        NOP\par
        NOP\par
        NOP\par
        ROR al, 1\par
    M1:    ROR bl, 9\par
        ADD al, bl\par
        ADD al, A\par
        ADD al, B\par
        JG M1\par
        DEC bl\par
        NOP\par
        NOP\par
        NOP\par
    Main endp\par
END\par
}
 