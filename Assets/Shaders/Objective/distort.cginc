float2 distortUV(float2 uv, float strength)
{
    float k1 = 1.2;
    float k2 = 1.0;
    float k3 = -3.2;

    uv -= 0.5;
    float len = length(uv);
    uv =  uv * k1
        + uv * len * k2
        + uv * len * len * k3;
    uv *= strength;
    uv += 0.5;

    return uv;
}