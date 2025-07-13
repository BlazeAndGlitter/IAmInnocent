// _half reference is important to choose precision of the functions we use, so we could write variants of this function in the same file - for example, with the suffix _float - so that Unity can use an alternative level of precision.
// MainLight is a single function
// The function is going to take in a WorldPos as input and output the Direction, Color and Attenuation of the light - but we need to define all four in the function parameters and specify which are outputs using the out keyword.
//(worldPos seems to include many properties, couldnt find a proper list of them, further more -> "The out keyword in C# allows a method to return multiple values by passing arguments by reference." -FV)
void MainLight_half(float3 WorldPos, out half3 Direction, out half3 Color, out half Attenuation)
{
	//we’ll provide “fake” output values to pretend there is a light in the preview window
#if SHADERGRAPH_PREVIEW
	Direction = half3(0.5, 0.5, 0);
	Color = 1;
	Attenuation = 1;
#else // For the in game rendering, we find out where the thing is in the world (WorldPos), and how that looks from the mainlights point of view
#if SHADOWS_SCREEN // use screen shadow
	half4 clipPos = TransformWorldToHClip(WorldPos);
	half4 shadowCoord = ComputeScreenPos(clipPos);
#else // use other kind of shadow
	half4 shadowCoord = TransformWorldToShadowCoord(WorldPos);
#endif 
	Light mainLight = GetMainLight(shadowCoord); // here we are getting the main light data with .direction, .color., distanceAttentuation and .shadowAttentuation
	Direction = mainLight.direction; // direction of the main light
	Color = mainLight.color; //how bright it is/color of the main light
	Attenuation = mainLight.distanceAttenuation * mainLight.shadowAttenuation; //calculating how much light is actually hitting the objet (light attenuation)
#endif
}