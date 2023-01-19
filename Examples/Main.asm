.286
.MODEL SMALL
.DATA 
    A DB 15 
    B DB 7 
.CODE 
    Main proc 
XOR al,al 
        XOR bl,bl
        NOP 
        NOP 
        NOP 
        ROR al,1 
  M1: ROR bl,9 
        ADD al,bl 
        ADD al,A 
        ADD al,B 
        JG M1 
        DEC bl 
        NOP 
        NOP 
        NOP 
    Main endp 
END