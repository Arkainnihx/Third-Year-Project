﻿public struct PopulationSpec {
    private int popSize;

    private float panicMultMin,
        panicMultMax,
        maxSpeedMin,
        maxSpeedMax,
        cautionMin,
        cautionMax,
        awarenessMin,
        awarenessMax;

    public PopulationSpec(int popSize, float panicMultMin, float panicMultMax, float maxSpeedMin, float maxSpeedMax,
        float cautionMin, float cautionMax, float awarenessMin, float awarenessMax) {
        this.popSize = popSize;
        this.panicMultMin = panicMultMin;
        this.panicMultMax = panicMultMax;
        this.maxSpeedMin = maxSpeedMin;
        this.maxSpeedMax = maxSpeedMax;
        this.cautionMin = cautionMin;
        this.cautionMax = cautionMax;
        this.awarenessMin = awarenessMin;
        this.awarenessMax = awarenessMax;
    }

    public PopulationSpec(int popSize) {
        this.popSize = popSize;
        panicMultMin = 0f;
        panicMultMax = 0;
        maxSpeedMin = 1;
        maxSpeedMax = 1;
        cautionMin = 1;
        cautionMax = 1;
        awarenessMin = 1;
        awarenessMax = 1;
    }

    public int PopSize {
        get { return popSize; }
    }

    public float PanicMultMin {
        get { return panicMultMin; }
    }

    public float PanicMultMax {
        get { return panicMultMax; }
    }

    public float MaxSpeedMin {
        get { return maxSpeedMin; }
    }

    public float MaxSpeedMax {
        get { return maxSpeedMax; }
    }

    public float CautionMin {
        get { return cautionMin; }
    }

    public float CautionMax {
        get { return cautionMax; }
    }

    public float AwarenessMin {
        get { return awarenessMin; }
    }

    public float AwarenessMax {
        get { return awarenessMax; }
    }
    
}