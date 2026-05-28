// Audio Data based on MOD SDK's AudioEvent and Multisound structures, adapted for Unity's ScriptableObject system
// Data is just a representation of the original XML data and may not include all properties or features from the original format

// Added Multisound that was used in FXList
// <FXList id="FX_GDIMammothExplode">
//  <NuggetList> <- Ommitted the NuggetList ParticleSystem structure, not needed for representation
//
//  <Sound Value="VehicleExplosionSmallMS" />
//      </NuggetList>
//  </FXList>

// for Clarity for myself of what was used so I dont get lost:

// VehicleExplosionSmallMS
// VehicleExplosionSmall_Close
// VehicleExplosionSmall_Distant
// VehicleExplosionSmall_Flange
// VehicleExplosionSmall_MediumDistant
// VehicleExplosionSmall_MetalPipe
// VehicleExplosionSmall_Unique
// VehicleExplosionCarMS
// VehicleExplosionCar_Close
// VehicleExplosionCar_MediumDistant
// VehicleExplosionCar_Unique
// GDI_Ox_VoiceCrash


// Gonna keep the same names for better reference

// Might utilize soundAmbient/soundMoveLoop later down the line 
// <AudioEntry Sound="PredatorIdleLoop" AudioType="soundAmbient"/>
// <AudioEntry Sound="GDI_GuardianAPC_MoveByLoop" AudioType="soundMoveLoop"/>

// Multisound is a collection of AudioEvents that can be played together, allowing for more complex and layered sound effects.
// It's an array of different AudioEvents that can are simultaneously

// <Multisound id="VehicleExplosionSmallMS">
// 		<Subsound>VehicleExplosionSmall_Close</Subsound>
// 		<Subsound>VehicleExplosionSmall_Distant</Subsound>
// 		<Subsound>VehicleExplosionSmall_Flange</Subsound>
// 		<Subsound>VehicleExplosionSmall_MediumDistant</Subsound>
// 		<Subsound>VehicleExplosionSmall_MetalPipe</Subsound>
// 		<Subsound>VehicleExplosionSmall_Unique</Subsound>
// 	</Multisound>

// Close, MediumDistant, Distant, Flange, MetalPipe, Unique
// Above Multisound is when CrashType is a Barrel

// AudioEvent represents a single sound event with properties like volume, pitch, delay, and control type (e.g., loop, interrupt)

// ReverbEffectLevel, DryLevel, Type, MinRange, MaxRange, Priority, aren't needed

// For VolumeShift and PitchShift, use a Vector2 to represent the low and high range for randomization, and then apply that random shift when playing the sound
// For Delay, also use a Vector2 to represent the low and high range for randomization of the delay before the sound plays, Original delay in XML is in Milliseconds, 
// but we can convert it to seconds for Unity's WaitForSeconds

// For Control and Limit, we can implement logic in the AudioManager to handle interrupting sounds or limiting the number of simultaneous instances of a sound based on the AudioControl type and limit value.

// Volume is represented as a percentage in the original XML, but we can convert it to a 0-1 range for Unity's AudioSource volume property.

// 	<AudioEvent id="VehicleExplosionSmall_Close" Volume="50%" VolumeShift="-15%" Limit="3" Type="WORLD SHROUDED EVERYONE" 
        // Control="INTERRUPT" MinRange="200" MaxRange="800" ReverbEffectLevel="100%" DryLevel="100%" SubmixSlider="SOUNDFX">
// 		<PitchShift Low="-10" High="10" />
// 		<Sound>WUVehic_explFGa</Sound>
// 		<Sound>WUVehic_explFGb</Sound>
// 		<Sound>WUVehic_explFGc</Sound>
// 		<Sound>WUVehic_explFGd</Sound>
// 		<Sound>WUVehic_explFGe</Sound>
// 		<Sound>WUVehic_explFGf</Sound>
// 		<Sound>WUVehic_explFGg</Sound>
// 		<Sound>WUVehic_explFGh</Sound>
// 		<Sound>WUVehic_explFGi</Sound>
// 	</AudioEvent>

// 	<AudioEvent id="VehicleExplosionSmall_Distant" Volume="70%" VolumeShift="-10%" Limit="3" Type="WORLD SHROUDED EVERYONE" 
    // Control="INTERRUPT" MinRange="200" MaxRange="800" ReverbEffectLevel="100%" DryLevel="100%" SubmixSlider="SOUNDFX">
// 		<PitchShift Low="-10" High="10" />
// 		<Sound>WUVehic_explBGa</Sound>
// 		<Sound>WUVehic_explBGb</Sound>
// 		<Sound>WUVehic_explBGc</Sound>
// 		<Sound>WUVehic_explBGd</Sound>
// 		<Sound>WUVehic_explBGe</Sound>
// 		<Sound>WUVehic_explBGf</Sound>
// 		<Sound>WUVehic_explBGg</Sound>
// 		<Sound>WUVehic_explBGh</Sound>
// 		<Sound>WUVehic_explBGi</Sound>
// 		<Sound>WUVehic_explBGj</Sound>
// 		<Sound>WUVehic_explBGk</Sound>
// 		<Sound>WUVehic_explBGl</Sound>
// 		<Sound>WUVehic_explBGm</Sound>
// 		<Sound>WUVehic_explBGn</Sound>
// 		<Sound>WUVehic_explBGo</Sound>
// 		<Sound>WUVehic_explBGp</Sound>
// 		<Sound>WUVehic_explBGq</Sound>
// 		<Sound>WUVehic_explBGr</Sound>
// 	</AudioEvent>

// 	<AudioEvent id="VehicleExplosionSmall_Flange" Volume="70%" VolumeShift="-10%" Limit="3" Type="WORLD SHROUDED EVERYONE" 
    // Control="INTERRUPT" MinRange="200" MaxRange="800" ReverbEffectLevel="100%" DryLevel="100%" SubmixSlider="SOUNDFX">
// 		<PitchShift Low="-10" High="10" />
// 		<Delay Low="1000" High="2000" />
// 		<Sound>WUVehic_explFlana</Sound>
// 		<Sound>WUVehic_explFlanb</Sound>
// 		<Sound>WUVehic_explFlanc</Sound>
// 		<Sound>WUVehic_explFland</Sound>
// 		<Sound>WUVehic_explFlane</Sound>
// 		<Sound>WUVehic_explFlanf</Sound>
// 		<Sound>WUVehic_explFlang</Sound>
// 		<Sound>WUVehic_explFlanh</Sound>
// 		<Sound>WUVehic_explFlani</Sound>
// 		<Sound>WUVehic_explFlanj</Sound>
// 		<Sound>WUVehic_explFlank</Sound>
// 		<Sound>WUVehic_explFlanl</Sound>
// 		<Sound>WUVehic_explFlanm</Sound>
// 	</AudioEvent>

// 	<AudioEvent id="VehicleExplosionSmall_MediumDistant" Volume="70%" VolumeShift="-10%" Limit="3" Type="WORLD SHROUDED EVERYONE" 
        // Control="INTERRUPT" MinRange="200" MaxRange="800" ReverbEffectLevel="100%" DryLevel="100%" SubmixSlider="SOUNDFX">
// 		<PitchShift Low="-10" High="10" />
// 		<Delay Low="0" High="500" />
// 		<Sound>WUVehic_explMGa</Sound>
// 		<Sound>WUVehic_explMGb</Sound>
// 		<Sound>WUVehic_explMGc</Sound>
// 		<Sound>WUVehic_explMGd</Sound>
// 		<Sound>WUVehic_explMGe</Sound>
// 		<Sound>WUVehic_explMGf</Sound>
// 		<Sound>WUVehic_explMGg</Sound>
// 		<Sound>WUVehic_explMGh</Sound>
// 		<Sound>WUVehic_explMGi</Sound>
// 		<Sound>WUVehic_explMGj</Sound>
// 		<Sound>WUVehic_explMGk</Sound>
// 		<Sound>WUVehic_explMGl</Sound>
// 		<Sound>WUVehic_explMGm</Sound>
// 		<Sound>WUVehic_explMGn</Sound>
// 		<Sound>WUVehic_explMGo</Sound>
// 		<Sound>WUVehic_explMGp</Sound>
// 		<Sound>WUVehic_explMGq</Sound>
// 		<Sound>WUVehic_explMGr</Sound>
// 	</AudioEvent>

// 	<AudioEvent id="VehicleExplosionSmall_MetalPipe" Volume="55%" VolumeShift="-10%" Limit="3" Type="WORLD SHROUDED EVERYONE" 
    // Control="INTERRUPT" MinRange="200" MaxRange="800" ReverbEffectLevel="100%" DryLevel="100%" SubmixSlider="SOUNDFX">
// 		<PitchShift Low="-50" High="0" />
// 		<Delay Low="0" High="1000" />
// 		<Sound>WUVehic_explMetaa</Sound>
// 		<Sound>WUVehic_explMetab</Sound>
// 		<Sound>WUVehic_explMetac</Sound>
// 		<Sound>WUVehic_explMetad</Sound>
// 		<Sound>WUVehic_explMetae</Sound>
// 		<Sound>WUVehic_explMetaf</Sound>
// 		<Sound>WUVehic_explMetag</Sound>
// 	</AudioEvent>

// 	<AudioEvent id="VehicleExplosionSmall_Unique" Volume="70%" VolumeShift="-10%" Limit="3" Type="WORLD SHROUDED EVERYONE" 
    // Control="INTERRUPT" MinRange="200" MaxRange="800" ReverbEffectLevel="100%" DryLevel="100%" SubmixSlider="SOUNDFX">
// 		<PitchShift Low="-10" High="10" />
// 		<Delay Low="500" High="1500" />
// 		<Sound>WUVehic_explUniqa</Sound>
// 		<Sound>WUVehic_explUniqb</Sound>
// 		<Sound>WUVehic_explUniqc</Sound>
// 		<Sound>WUVehic_explUniqd</Sound>
// 		<Sound>WUVehic_explUniqe</Sound>
// 		<Sound>WUVehic_explUniqf</Sound>
// 		<Sound>WUVehic_explUniqg</Sound>
// 		<Sound>WUVehic_explUniqh</Sound>
// 		<Sound>WUVehic_explUniqi</Sound>
// 		<Sound>WUVehic_explUniqj</Sound>
// 		<Sound>WUVehic_explUniqk</Sound>
// 	</AudioEvent>

// SubmixSlider is a property that determines which audio mixer group the sound belongs to, 
// allowing for different processing and effects (like volume) based on the type of sound (e.g., music, sound effects, dialogue)

// Voice Crash played when CrashType is a Vehicle
// Similarly to VehicleExplosionCarMS

// PlayPercent is a property that determines the chance of playing a sound when event triggered, but I don't want it here

// 	<AudioEvent id="GDI_Ox_VoiceCrash" Volume="70%" PlayPercent="30%" Limit="1" Priority="LOW" Type="WORLD SHROUDED VOICE EVERYONE" 
//  MinRange="200" MaxRange="800" ReverbEffectLevel="100%" DryLevel="100%" SubmixSlider="VOICE">
// 		<PitchShift Low="-1" High="1" />
// 		<Sound>GUOxTra_VoiCrasha</Sound> // WE'RE LOSING CONTROL!
// 		// <Sound>GUOxTra_VoiCrashb</Sound> // WE'RE GOING DOWN! < -- This one is an odd line to use here, the whole audioevent is about the pilot screaming about his aicraft crashing down,
                                                    // but we have a ground vehicle here not an aircraft, it'd just sound weird
// 		<Sound>GUOxTra_VoiCrashc</Sound> // I KNEW THIS WOULD HAPPEN!
// 		<Sound>GUOxTra_VoiCrashd</Sound> // NOOOOO!
// 		<Sound>GUOxTra_VoiCrashe</Sound> // OH NO!.. OH NOO!
// 	</AudioEvent>

// Better than Using Verti sounds :
// <AudioEvent id="NOD_VertigoBomber_VoiceCrash" Volume="70%" PlayPercent="30%" Limit="1" Priority="LOW" Type="WORLD SHROUDED VOICE EVERYONE" MinRange="200" MaxRange="800" ReverbEffectLevel="100%" DryLevel="100%" SubmixSlider="SOUNDFX">
//     <PitchShift Low="-1" High="1" />
//     <Sound>NuVerti_VoiCrasha</Sound> // IT'S OVER!
//     <Sound>NuVerti_VoiCrashb</Sound> // NO NO.. NOO!!
//     <Sound>NuVerti_VoiCrashc</Sound> // whoops <-- le funni line that could be used as easter egg joke when hitting a barrel
// </AudioEvent>

// 	<AudioEvent id="VehicleExplosionCar_Close" Volume="45%" VolumeShift="-15%" Limit="3" Type="WORLD SHROUDED EVERYONE"
    // Control="INTERRUPT" MinRange="200" MaxRange="800" ReverbEffectLevel="100%" DryLevel="100%" SubmixSlider="SOUNDFX">
// 		<PitchShift Low="-10" High="10" />
// 		<Delay Low="0" High="10" />
// 		<Sound>WUVehic_explFGa</Sound>
// 		<Sound>WUVehic_explFGb</Sound>
// 		<Sound>WUVehic_explFGc</Sound>
// 		<Sound>WUVehic_explFGd</Sound>
// 		<Sound>WUVehic_explFGe</Sound>
// 		<Sound>WUVehic_explFGf</Sound>
// 		<Sound>WUVehic_explFGg</Sound>
// 		<Sound>WUVehic_explFGh</Sound>
// 		<Sound>WUVehic_explFGi</Sound>
// 	</AudioEvent>

// 	<AudioEvent id="VehicleExplosionCar_MediumDistant" Volume="65%" VolumeShift="-10%" Limit="3" Type="WORLD SHROUDED EVERYONE" 
    // Control="INTERRUPT" MinRange="200" MaxRange="800" ReverbEffectLevel="100%" DryLevel="100%" SubmixSlider="SOUNDFX">
// 	<PitchShift Low="-10" High="10" />
// 		<Delay Low="20" High="100" />
// 		<Sound>WUVehic_explMGa</Sound>
// 		<Sound>WUVehic_explMGb</Sound>
// 		<Sound>WUVehic_explMGc</Sound>
// 		<Sound>WUVehic_explMGd</Sound>
// 		<Sound>WUVehic_explMGe</Sound>
// 		<Sound>WUVehic_explMGf</Sound>
// 		<Sound>WUVehic_explMGg</Sound>
// 		<Sound>WUVehic_explMGh</Sound>
// 		<Sound>WUVehic_explMGi</Sound>
// 		<Sound>WUVehic_explMGj</Sound>
// 		<Sound>WUVehic_explMGk</Sound>
// 		<Sound>WUVehic_explMGl</Sound>
// 		<Sound>WUVehic_explMGm</Sound>
// 		<Sound>WUVehic_explMGn</Sound>
// 		<Sound>WUVehic_explMGo</Sound>
// 		<Sound>WUVehic_explMGp</Sound>
// 		<Sound>WUVehic_explMGq</Sound>
// 		<Sound>WUVehic_explMGr</Sound>
// 	</AudioEvent>

// 	<AudioEvent id="VehicleExplosionCar_Unique" Volume="65%" VolumeShift="-10%" Limit="3" Type="WORLD SHROUDED EVERYONE" 
    // Control="INTERRUPT" MinRange="200" MaxRange="800" ReverbEffectLevel="100%" DryLevel="100%" SubmixSlider="SOUNDFX">
// 		<PitchShift Low="-10" High="10" />
// 		<Delay Low="10" High="50" />
// 		<Sound>WUVehic_explUniqa</Sound>
// 		<Sound>WUVehic_explUniqb</Sound>
// 		<Sound>WUVehic_explUniqc</Sound>
// 		<Sound>WUVehic_explUniqd</Sound>
// 		<Sound>WUVehic_explUniqe</Sound>
// 		<Sound>WUVehic_explUniqf</Sound>
// 		<Sound>WUVehic_explUniqg</Sound>
// 		<Sound>WUVehic_explUniqh</Sound>
// 		<Sound>WUVehic_explUniqi</Sound>
// 		<Sound>WUVehic_explUniqj</Sound>
// 		<Sound>WUVehic_explUniqk</Sound>
// 	</AudioEvent>

// 	<Multisound id="VehicleExplosionCarMS">
// 		<Subsound>VehicleExplosionCar_Close</Subsound>
// 		<Subsound>VehicleExplosionCar_MediumDistant</Subsound>
// 		<Subsound>VehicleExplosionCar_Unique</Subsound>
// 	</Multisound>

// Hmm, might be worth added ClientBehaviors too?

// ClientBehaviors are used to trigger sounds based on certain conditions, such as animations or model states. 
// They can be used to create more dynamic and responsive audio experiences in the game. For example, a 
// ModelConditionAudioLoopClientBehavior could be used to play a looping sound when a certain model condition is met, 
// such as a character being paralyzed or dying. An AnimationSoundClientBehavior could be used to play specific sounds at certain frames 
// of an animation, such as footsteps during a walking animation.

// Example ClientBehaviors that could be added based on the original XML data:
// <ClientBehaviors>
    //     <ModelConditionAudioLoopClientBehavior id="MCALCB">
    //         <ModelConditionSound Sound="NOD_TerrorDrone_WeaponGrinder" RequiredFlags="USER_60" ExcludedFlags="PARALYZED DYING"/>
    //     </ModelConditionAudioLoopClientBehavior>
    //     <AnimationSoundClientBehavior id="ModuleTag_ASCB">
    //         <Sound Sound="GDI_Juggernaught_Footstep" Animation="GUCOLOSSUS_BIFA" Frame="0 40"/>
    //         <Sound Sound="GDI_Juggernaught_Footstep" Animation="GUCOLOSSUS_BIFB" Frame="0 75"/>
    //     </AnimationSoundClientBehavior>
    // </ClientBehaviors>

// Might be well fitting for TURN_LEFT and TURN_RIGHT anim trigger events for player car :D

// <ModelConditionSound Sound="VEHICLE_TURN_LEFT" RequiredFlags="TURN_LEFT"/>

// or maybe we can even control it further

// Example anim with 11 frames
// <Sound Sound="VEHICLE_TURN_LEFT" Animation="TURN_LEFT_ANIM" Frame="0"/>, 
// <Sound Sound="VEHICLE_TURN_LEFT_END" Animation="TURN_LEFT_ANIM" Frame="10"/>, 

// and then the AudioEvent data could be a fade in/fade out effect for the turning sound
// TURN_LEFT having fadein, and TURN_LEFT_END having fadeout, and then the AudioManager can handle the fade effect

//000----------------------------------------

// Time for some ambiancee

// Ambient sounds can be used to create a more immersive environment in the game, such as the sound of wind and birds
// Set to LOOP control usually

// PerFilePitchShift and PerFileVolumeShift are properties that allow for random variation in pitch and volume for each individual sound file within an AudioEvent,
// not the entire AudioEvent, but will treat it like normal VolumeShift and PitchShift

// <AudioEvent id="Amb_Birds1" Volume="70%" PerFileVolumeShift="-5%" Limit="3" Priority="LOWEST" Type="WORLD SHROUDED EVERYONE" 
    //Control="LOOP" MinRange="400" MaxRange="900" ReverbEffectLevel="100%" DryLevel="100%" SubmixSlider="AMBIENT">
// 	<PerFilePitchShift Low="-5" High="5" />
// 	<Delay Low="3000" High="8000" />
// 	<Sound>WABirds_amonh1a</Sound>
// 	<Sound>WABirds_amonh1b</Sound>
// 	<Sound>WABirds_amonh1c</Sound>
// 	<Sound>WABirds_amonh1d</Sound>
// 	<Sound>WABirds_amonh1e</Sound>
// 	<Sound>WABirds_amonh1f</Sound>
// 	<Sound>WABirds_amonh1g</Sound>
// 	<Sound>WABirds_amonh1h</Sound>
// 	<Sound>WABirds_amonh1i</Sound>
// 	<Sound>WABirds_amonh1j</Sound>
// </AudioEvent>

// <AudioEvent id="Amb_Birds2" Volume="70%" PerFileVolumeShift="-10%" Limit="3" Priority="LOWEST" Type="WORLD SHROUDED EVERYONE" 
    //Control="LOOP" MinRange="400" MaxRange="900" ReverbEffectLevel="100%" DryLevel="100%" SubmixSlider="AMBIENT">
//     <PerFilePitchShift Low="-5" High="5" />
//     <Delay Low="3000" High="8000" />
//     <Sound>WABirds_amonh2a</Sound>
//     <Sound>WABirds_amonh2b</Sound>
//     <Sound>WABirds_amonh2c</Sound>
//     <Sound>WABirds_amonh2d</Sound>
//     <Sound>WABirds_amonh2e</Sound>
//     <Sound>WABirds_amonh2f</Sound>
//     <Sound>WABirds_amonh2g</Sound>
//     <Sound>WABirds_amonh2h</Sound>
//     <Sound>WABirds_amonh2i</Sound>
//     <Sound>WABirds_amonh2j</Sound>
// </AudioEvent>

// Hmm, add a crow too ig, why not
// Crow Squak fits best here i think

// <AudioEvent id="Amb_BirdsCrowSquak" Volume="70%" PerFileVolumeShift="-15%" Limit="3" Priority="LOWEST" 
// Type="WORLD SHROUDED EVERYONE" Control="LOOP" MinRange="400" MaxRange="900" ReverbEffectLevel="100%" DryLevel="100%" 
    // SubmixSlider="AMBIENT">
// 	<PerFilePitchShift Low="-5" High="5" />
// 	<Delay Low="4000" High="11000" />
// 	<Sound>WABirds_crow3a</Sound>
// 	<Sound>WABirds_crow3b</Sound>
// 	<Sound>WABirds_crow3c</Sound>
// 	<Sound>WABirds_crow3d</Sound>
// 	<Sound>WABirds_crow3e</Sound>
// </AudioEvent>