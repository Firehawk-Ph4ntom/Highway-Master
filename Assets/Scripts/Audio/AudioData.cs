// Audio Data based on SAGE MOD SDK's AudioEvent and Multisound structures, adapted for Unity's ScriptableObject system
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

// Amb_Birds1
// Amb_Birds2
// Amb_BirdsCrowSquak
// Amb_BirdsCrowSimple
// AmbStream_BlueZoneA_5point1
// AmbStream_BlueZone03_Pristine_5point1
// Amb_WoodCreakTree1

// SmallGenericBuilding_Die_MS
// SmallGenericBuilding_Explosion
// SmallGenericBuilding_GlassExplosion_Delayed
// SmallGenericBuilding_MetalExplosion_Delayed
// SmallGenericBuilding_UniqueExplosion
// SmallGenericBuilding_WoodExplosion_Delayed

// Amb_DirtFall


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

// Added some sounds from XCC Mixer for some OOOMPH :D (Not part of SAGE Assets)
// vgendiea, vgendieb, vgendiec, vgendied, vgendiee, vgendief
// AudioEvent for them made similar to VehicleExplosionSmall_Close, renamed to VehicleExplosionSmall_Die

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

// <AudioEvent id="NOD_VertigoBomber_VoiceCrash" Volume="70%" PlayPercent="30%" Limit="1" Priority="LOW" 
    // Type="WORLD SHROUDED VOICE EVERYONE" MinRange="200" MaxRange="800" ReverbEffectLevel="100%" DryLevel="100%" SubmixSlider="VOICE">
//     <PitchShift Low="-1" High="1" />
//     <Sound>NuVerti_VoiCrasha</Sound> // IT'S OVER!
//     <Sound>NuVerti_VoiCrashb</Sound> // NO NO.. NOO!!
//     <Sound>NuVerti_VoiCrashc</Sound> // whoops <-- le funni line that could be used as easter egg joke when hitting a barrel
// </AudioEvent>

//actually,
// After thinking, might just use wilheim screams instead (much funnier too) better than using ox and verti sounds
// Take Ox Crash AudioEvent and give it NUYELL audiofiles, and then rename AudioEvent to Car_VoiceCrash

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
// Set to LOOP control usually, but for this scenario it should be INTERRUPT because AudioManager is persistent,
// 

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

// <AudioEvent id="Amb_BirdsCrowSimple" Volume="70%" PerFileVolumeShift="-15%" Limit="3" Priority="LOWEST" 
// Type="WORLD SHROUDED EVERYONE" Control="LOOP" MinRange="400" MaxRange="900" ReverbEffectLevel="100%" DryLevel="100%" 
    // SubmixSlider="AMBIENT">
//     <PerFilePitchShift Low="-5" High="5" />
//     <Delay Low="4000" High="11000" />
//     <Sound>WABirds_crow4a</Sound>
//     <Sound>WABirds_crow4b</Sound>
//     <Sound>WABirds_crow4c</Sound>
//     <Sound>WABirds_crow4d</Sound>
//     <Sound>WABirds_crow4e</Sound>
// </AudioEvent>

// Winddddd
// The following 2 events should be streamed not loaded into memory directly due to fileSize

//<AmbientStream id="AmbStream_BlueZoneA_5point1" Volume="45%" Priority="CRITICAL" Type="EVERYONE" Control="RANDOMSTART ALLOW_KILL_MID_FILE" 
// 	MinRange="3400" MaxRange="4400" DryLevel="100%" SubmixSlider="AMBIENT">
// 	<Filename>WABlue_Zonea_5point1</Filename>
// </AmbientStream>

// <AmbientStream id="AmbStream_BlueZone03_Pristine_5point1" Volume="35%" Priority="CRITICAL" Type="EVERYONE" Control="RANDOMSTART ALLOW_KILL_MID_FILE" 
// 	MinRange="3400" MaxRange="4400" DryLevel="100%" SubmixSlider="AMBIENT">
// 	<Filename>WABlue_Zone03_5point1</Filename>
// </AmbientStream>

// <AudioEvent id="Amb_WoodCreakTree1" Volume="70%" PerFileVolumeShift="-15%" Limit="3" Priority="LOWEST" 
    // Type="WORLD SHROUDED EVERYONE" Control="LOOP" MinRange="400" MaxRange="900" ReverbEffectLevel="100%" DryLevel="100%" 
    // SubmixSlider="AMBIENT">
//     <PerFilePitchShift Low="-10" High="5" />
//     <Delay Low="3000" High="8000" />
//     <Sound>WACreak_tree1_a</Sound>
//     <Sound>WACreak_tree1_b</Sound>
//     <Sound>WACreak_tree1_c</Sound>
//     <Sound>WACreak_tree1_d</Sound>
//     <Sound>WACreak_tree1_e</Sound>
//     <Sound>WACreak_tree1_f</Sound>
//     <Sound>WACreak_tree1_g</Sound>
//     <Sound>WACreak_tree1_h</Sound>
//     <Sound>WACreak_tree1_i</Sound>
//     <Sound>WACreak_tree1_j</Sound>
//     <Sound>WACreak_tree1_k</Sound>
//     <Sound>WACreak_tree1_l</Sound>
//     <Sound>WACreak_tree1_m</Sound>
//     <Sound>WACreak_tree1_n</Sound>
// </AudioEvent>

//0-------------------

// Gonna use Generic small building die audio for car crash
// BaseSoundEffect baseInheritance attributes aren't needed for this project (like the min-max range, reverb/dry level, priority, etc),
// and mostly contain other base info that got overridden anyway

// Unused
// <AudioEvent id="SmallGenericBuilding_Die" inheritFrom="AudioEvent:BaseSoundEffect"
//     Volume = "80"
//     VolumeShift = "-10"
//     Control = "INTERRUPT"
//     Limit = "3"
//     Type = "WORLD SHROUDED EVERYONE"
//     SubmixSlider = "SOUNDFX" >
//     <PitchShift Low = "0" High = "20" />
//     <Sound>WIBuild_diea</Sound> <Sound>WIBuild_dieb</Sound> <Sound>WIBuild_diec</Sound> <Sound>WIBuild_died</Sound> <Sound>WIBuild_diee</Sound> 
//     <Sound>WIBuild_dief</Sound> <Sound>WIBuild_dieg</Sound> <Sound>WIBuild_dieh</Sound> <Sound>WIBuild_diei</Sound>
// </AudioEvent>

// <AudioEvent id="SmallGenericBuilding_Explosion" inheritFrom="AudioEvent:BaseSoundEffect"
//     Volume = "80"
//     VolumeShift = "-10"
//     Control = "INTERRUPT"
//     Limit = "3"
//     Type = "WORLD SHROUDED EVERYONE"
//     SubmixSlider = "SOUNDFX" >
//     <PitchShift Low = "-20" High = "-10" />
//     <Sound>WIExplo_geneLarg1a</Sound> <Sound>WIExplo_geneLarg1b</Sound> <Sound>WIExplo_geneLarg1c</Sound> <Sound>WIExplo_geneLarg1d</Sound> 
//     <Sound>WIExplo_geneLarg1e</Sound> <Sound>WIExplo_geneLarg1f</Sound>
// </AudioEvent>

// <AudioEvent id="SmallGenericBuilding_GlassExplosion_Delayed" inheritFrom="AudioEvent:BaseSoundEffect"
//     Volume = "40"
//     VolumeShift = "-10"
//     Control = "INTERRUPT"
//     Limit = "3"
//     Type = "WORLD SHROUDED EVERYONE"
//     SubmixSlider = "SOUNDFX" >
//     <PitchShift Low = "-10" High = "25" />
//     <Delay Low = "1500" High = "2500" /> <-- delay too long, lowered it in Unity to 500-1000ms
//     <Sound>WIExplo_glasLarg1a</Sound> <Sound>WIExplo_glasLarg1b</Sound> <Sound>WIExplo_glasLarg1c</Sound> <Sound>WIExplo_glasLarg1d</Sound> 
//     <Sound>WIExplo_glasLarg1e</Sound> <Sound>WIExplo_glasLarg1f</Sound>
// </AudioEvent>

// <AudioEvent id="SmallGenericBuilding_MetalExplosion_Delayed" inheritFrom="AudioEvent:BaseSoundEffect"
//     Volume = "40"
//     VolumeShift = "-10"
//     Control = "INTERRUPT"
//     Limit = "3"
//     Type = "WORLD SHROUDED EVERYONE"
//     SubmixSlider = "SOUNDFX" >
//     <PitchShift Low = "-10" High = "25" />
//     <Delay Low = "1500" High = "2500" /> <-- delay too long, lowered it in Unity to 500-1000ms
//     <Sound>WIExplo_metaLarg1a</Sound> <Sound>WIExplo_metaLarg1b</Sound> <Sound>WIExplo_metaLarg1c</Sound> <Sound>WIExplo_metaLarg1d</Sound> 
//     <Sound>WIExplo_metaLarg1e</Sound> <Sound>WIExplo_metaLarg1f</Sound>
// </AudioEvent>

// <AudioEvent id="SmallGenericBuilding_UniqueExplosion" inheritFrom="AudioEvent:BaseSoundEffect"
//     Volume = "80"
//     VolumeShift = "-10"
//     Control = "INTERRUPT"
//     Limit = "3"
//     Type = "WORLD SHROUDED EVERYONE"
//     SubmixSlider = "SOUNDFX" >
//     <PitchShift Low = "0" High = "20" />
//     <Sound>WIExplo_uniqLarg1a</Sound> <Sound>WIExplo_uniqLarg1b</Sound> <Sound>WIExplo_uniqLarg1c</Sound> <Sound>WIExplo_uniqLarg1d</Sound> 
//     <Sound>WIExplo_uniqLarg1e</Sound> <Sound>WIExplo_uniqLarg1f</Sound>
// </AudioEvent>

// <AudioEvent id="SmallGenericBuilding_WoodExplosion_Delayed" inheritFrom="AudioEvent:BaseSoundEffect"
//     Volume = "40"
//     VolumeShift = "-10"
//     PlayPercent = "25"
//     Control = "INTERRUPT"
//     Limit = "3"
//     Type = "WORLD SHROUDED EVERYONE"
//     SubmixSlider = "SOUNDFX" >
//     <PitchShift Low = "-10" High = "25" />
//     <Delay Low = "1500" High = "2500" /> <-- delay too long, lowered it in Unity to 500-1000ms
//     <Sound>WIExplo_woodLarg1a</Sound> <Sound>WIExplo_woodLarg1b</Sound>
// </AudioEvent>


// <Multisound id="SmallGenericBuilding_Die_MS" >
//     <Subsound>SmallGenericBuilding_Die</Subsound> <-- Decided not to use it, too long

//     <Subsound>SmallGenericBuilding_Explosion</Subsound> 
//     <Subsound>SmallGenericBuilding_GlassExplosion_Delayed</Subsound> 
//     <Subsound>SmallGenericBuilding_MetalExplosion_Delayed</Subsound> 
//     <!-- <Subsound>SmallGenericBuilding_UniqueExplosion</Subsound> --> <-- commented out in original xml by EA, but i'll use it here because why not
//     <Subsound>SmallGenericBuilding_WoodExplosion_Delayed</Subsound> 
// </Multisound>

// For hole crashes, we need to add some dirt/rock falling sounds, simulating falling debris

// <AudioEvent id="Amb_DirtFall" Volume="35%" PerFileVolumeShift="-10%" Limit="3" Priority="LOWEST" 
    //Type="WORLD SHROUDED EVERYONE" Control="LOOP" MinRange="400" MaxRange="900" ReverbEffectLevel="100%" DryLevel="100%" 
    // SubmixSlider="AMBIENT"> <-- In this case it should be SOUNDFX instead cause it's not used as an ambient audio
//     <PerFilePitchShift Low="-5" High="5" />
//     <Delay Low="3000" High="10000" />
//     <Sound>WADirt_fall_1a</Sound>
//     <Sound>WADirt_fall_1b</Sound>
//     <Sound>WADirt_fall_1c</Sound>
//     <Sound>WADirt_fall_1d</Sound>
//     <Sound>WADirt_fall_1e</Sound>
//     <Sound>WADirt_fall_1f</Sound>
//     <Sound>WADirt_fall_1g</Sound>
//     <Sound>WADirt_fall_1h</Sound>
// </AudioEvent>

// <AudioEvent id="Amb_RockTumbleMedium" Volume="80%" PerFileVolumeShift="-10%" Limit="3" Priority="LOWEST" 
//         Type="WORLD SHROUDED EVERYONE" Control="LOOP" MinRange="450" MaxRange="1000" ReverbEffectLevel="100%" DryLevel="100%" 
//         SubmixSlider="AMBIENT"> <-- In this case it should be SOUNDFX instead cause it's not used as an ambient audio
//     <PerFilePitchShift Low="-10" High="5" />
//     <Delay Low="3000" High="8000" />
//     <Sound>WARock_tumbl1a</Sound>
//     <Sound>WARock_tumbl1b</Sound>
//     <Sound>WARock_tumbl1c</Sound>
//     <Sound>WARock_tumbl1d</Sound>
//     <Sound>WARock_tumbl1e</Sound>
//     <Sound>WARock_tumbl1f</Sound>
//     <Sound>WARock_tumbl1g</Sound>
//     <Sound>WARock_tumbl1h</Sound>
//     <Sound>WARock_tumbl1i</Sound>
//     <Sound>WARock_tumbl1j</Sound>
//     <Sound>WARock_tumbl1k</Sound>
//     <Sound>WARock_tumbl1l</Sound>
//     <Sound>WARock_tumbl1m</Sound>
//     <Sound>WARock_tumbl1n</Sound>
//     <Sound>WARock_tumbl1o</Sound>
//     <Sound>WACreak_misc1a</Sound>
//     <Sound>WACreak_misc1b</Sound>
// </AudioEvent>

// <AudioEvent id="Bodyfall" Volume="25%" VolumeShift="-10%" Limit="3" Priority="LOW" Type="WORLD SHROUDED EVERYONE" 
    //Control="INTERRUPT" MinRange="200" MaxRange="800" ReverbEffectLevel="100%" DryLevel="100%" SubmixSlider="SOUNDFX">
//     <PitchShift Low="-5" High="5" />
//     <Delay Low="0" High="50" />
//     <Sound>WImpac_bodyfalla</Sound>
//     <Sound>WImpac_bodyfallb</Sound>
//     <Sound>WImpac_bodyfallc</Sound>
//     <Sound>WImpac_bodyfalld</Sound>
//     <Sound>WImpac_bodyfalle</Sound>
//     <Sound>WImpac_bodyfallf</Sound>
//     <Sound>WImpac_bodyfallg</Sound>
//     <Sound>WImpac_bodyfallh</Sound>
//     <Sound>WImpac_bodyfalli</Sound>
//     <Sound>WImpac_bodyfallj</Sound>
//     <Sound>WImpac_bodyfallk</Sound>
//     <Sound>WImpac_bodyfalll</Sound>
//     <Sound>WImpac_bodyfallm</Sound>
//     <Sound>WImpac_bodyfalln</Sound>
// </AudioEvent>

// <AudioEvent id="BodyDirt" Volume="25%" VolumeShift="-10%" Limit="3" Priority="LOW" Type="WORLD SHROUDED EVERYONE" 
    //Control="INTERRUPT" MinRange="200" MaxRange="800" ReverbEffectLevel="100%" DryLevel="100%" SubmixSlider="SOUNDFX">
//     <PitchShift Low="-5" High="5" />
//     <Sound>WImpac_bodyfalla</Sound>
//     <Sound>WImpac_bodyfallb</Sound>
//     <Sound>WImpac_bodyfallc</Sound>
//     <Sound>WImpac_bodyfalld</Sound>
//     <Sound>WImpac_bodyfalle</Sound>
//     <Sound>WImpac_bodyfallf</Sound>
//     <Sound>WImpac_bodyfallg</Sound>
//     <Sound>WImpac_bodyfallh</Sound>
//     <Sound>WImpac_bodyfalli</Sound>
//     <Sound>WImpac_bodyfallj</Sound>
//     <Sound>WImpac_bodyfallk</Sound>
//     <Sound>WImpac_bodyfalll</Sound>
//     <Sound>WImpac_bodyfallm</Sound>
//     <Sound>WImpac_bodyfalln</Sound>
// </AudioEvent>\

// Loop Sounds for cars' engines

// Car A
// <AudioEvent id="NOD_AttackBike_IdleLoop" Volume="60%" VolumeShift="-5%" Limit="3" Priority="LOWEST" 
// Type="WORLD SHROUDED EVERYONE" Control="LOOP FADE_ON_KILL FADE_ON_START" MinRange="30" MaxRange="250"
    //  ReverbEffectLevel="100%" DryLevel="100%" SubmixSlider="SOUNDFX">
//     <PitchShift Low="-5" High="5" />
//     <Sound>NUAttac_idleLoopa</Sound>
//     <Sound>NUAttac_idleLoopb</Sound>
//     <Sound>NUAttac_idleLoopc</Sound>
//     <Sound>NUAttac_idleLoopd</Sound>
//     <Sound>NUAttac_idleLoope</Sound>
//     <Sound>NUAttac_idleLoopf</Sound>
//     <Sound>NUAttac_idleLoopg</Sound>
//     <Sound>NUAttac_idleLooph</Sound>
//     <Sound>NUAttac_idleLoopi</Sound>
//     <Sound>NUAttac_idleLoopj</Sound>
//     <Sound>NUAttac_idleLoopk</Sound>
//     <Sound>NUAttac_idleLoopl</Sound>
//     <Sound>NUAttac_idleLoopm</Sound>
//     <Sound>NUAttac_idleLoopn</Sound>
//     <Sound>NUAttac_idleLoopo</Sound>
//     <Sound>NUAttac_idleLoopp</Sound>
//     <Sound>NUAttac_idleLoopq</Sound>
//     <Sound>NUAttac_idleLoopr</Sound>
//     <Sound>NUAttac_idleLoops</Sound>
//     <Sound>NUAttac_idleLoopt</Sound>
// </AudioEvent>

// Car B
// <AudioEvent id="NOD_ScorpionTank_IdleLoop" Volume="27%" VolumeShift="-5%" Limit="3" Priority="LOWEST" 
// 	Type="WORLD SHROUDED EVERYONE" Control="LOOP FADE_ON_KILL FADE_ON_START" MinRange="30" MaxRange="250"
// 	 ReverbEffectLevel="100%" DryLevel="100%" SubmixSlider="SOUNDFX">
// 	<PitchShift Low="-5" High="5" />
// 	<Sound>NUScorp_idleLoopa</Sound>
// 	<Sound>NUScorp_idleLoopb</Sound>
// 	<Sound>NUScorp_idleLoopc</Sound>
// 	<Sound>NUScorp_idleLoopd</Sound>
// </AudioEvent>

// Motorcycle 
// <AudioEvent id="NOD_RaiderBuggy_IdleLoop" Volume="25%" VolumeShift="-5%" Limit="3" Priority="LOWEST" 
// Type="WORLD SHROUDED EVERYONE" Control="LOOP FADE_ON_KILL FADE_ON_START" MinRange="30" MaxRange="250" ReverbEffectLevel="100%" 
// DryLevel="100%" SubmixSlider="SOUNDFX">
// 	<PitchShift Low="-5" High="5" />
// 	<Sound>NURaide_idleLoopa</Sound>
// 	<Sound>NURaide_idleLoopb</Sound>
// 	<Sound>NURaide_idleLoopc</Sound>
// 	<Sound>NURaide_idleLoopd</Sound>
// 	<Sound>NURaide_idleLoope</Sound>
// 	<Sound>NURaide_idleLoopf</Sound>
// 	<Sound>NURaide_idleLoopg</Sound>
// 	<Sound>NURaide_idleLooph</Sound>
// </AudioEvent>

// Jeep A
// <AudioEvent id="GDI_Surveyor_IdleLoop" Volume="30%" VolumeShift="-5%" Limit="3" Priority="LOWEST" 
// Type="WORLD SHROUDED EVERYONE" Control="LOOP FADE_ON_KILL FADE_ON_START" MinRange="30" MaxRange="250" ReverbEffectLevel="100%" 
// DryLevel="100%" SubmixSlider="SOUNDFX">
//     <PitchShift Low="-5" High="5" />
//     <Sound>GUSurve_idleLoopa</Sound>
//     <Sound>GUSurve_idleLoopb</Sound>
//     <Sound>GUSurve_idleLoopc</Sound>
//     <Sound>GUSurve_idleLoopd</Sound>
//     <Sound>GUSurve_idleLoope</Sound>
//     <Sound>GUSurve_idleLoopf</Sound>
//     <Sound>GUSurve_idleLoopg</Sound>
// </AudioEvent>

// Jeep B
// <AudioEvent id="GDI_Marv_IdleLoop" Volume="45%" VolumeShift="-5%" Limit="3" Priority="LOWEST" 
// Type="WORLD SHROUDED EVERYONE" Control="LOOP FADE_ON_KILL FADE_ON_START" MinRange="30" MaxRange="250" ReverbEffectLevel="100%" 
// DryLevel="100%" SubmixSlider="SOUNDFX">
// 	<PitchShift Low="-5" High="5" />
// 	<Sound>GUMarv_idleLoopa</Sound>
// 	<Sound>GUMarv_idleLoopb</Sound>
// 	<Sound>GUMarv_idleLoopc</Sound>
// 	<Sound>GUMarv_idleLoopd</Sound>
// 	<Sound>GUMarv_idleLoope</Sound>
// </AudioEvent>

// Pickup truck
// <AudioEvent id="NOD_Reckoner_IdleLoop" Volume="30%" VolumeShift="-5%" Limit="3" Priority="LOWEST" 
// Type="WORLD SHROUDED EVERYONE" Control="LOOP FADE_ON_KILL FADE_ON_START" MinRange="100" MaxRange="300" ReverbEffectLevel="100%" 
// DryLevel="100%" SubmixSlider="SOUNDFX">
//     <PitchShift Low="-10" High="10" />
//     <Sound>NURecko_idleLoopa</Sound>
//     <Sound>NURecko_idleLoopb</Sound>
//     <Sound>NURecko_idleLoopc</Sound>
// </AudioEvent>

// Bus
// <AudioEvent id="GDI_RepairAPC_IdleLoop" Volume="45%" VolumeShift="-5%" Limit="3" Priority="LOWEST" 
// Type="WORLD SHROUDED EVERYONE" Control="LOOP FADE_ON_KILL FADE_ON_START" MinRange="100" MaxRange="300" ReverbEffectLevel="100%" 
// DryLevel="100%" SubmixSlider="SOUNDFX">
//     <PitchShift Low="-5" High="5" />
//     <Sound>GURepai_idleLoopa</Sound>
//     <Sound>GURepai_idleLoopb</Sound>
//     <Sound>GURepai_idleLoopc</Sound>
//     <Sound>GURepai_idleLoopd</Sound>
//     <Sound>GURepai_idleLoope</Sound>
//     <Sound>GURepai_idleLoopf</Sound>
//     <Sound>GURepai_idleLoopg</Sound>
// </AudioEvent>

// trailer truck
// <AudioEvent id="GDI_PredatorTank_IdleLoop" Volume="18%" VolumeShift="-5%" Limit="3" Priority="LOWEST" Type="WORLD SHROUDED EVERYONE" 
// Control="LOOP FADE_ON_KILL FADE_ON_START" MinRange="30" MaxRange="250" ReverbEffectLevel="100%" DryLevel="100%" 
// SubmixSlider="SOUNDFX">
//     <PitchShift Low="-5" High="5" />
//     <Sound>GUPreda_idleL2a</Sound>
//     <Sound>GUPreda_idleL2b</Sound>
//     <Sound>GUPreda_idleL2c</Sound>
//     <Sound>GUPreda_idleL2d</Sound>
//     <Sound>GUPreda_idleL2e</Sound>
//     <Sound>GUPreda_idleL2f</Sound>
//     <Sound>GUPreda_idleL2g</Sound>
//     <Sound>GUPreda_idleL2h</Sound>
//     <Sound>GUPreda_idleL2i</Sound>
//     <Sound>GUPreda_idleL2j</Sound>
//     <Sound>GUPreda_idleL2k</Sound>
//     <Sound>GUPreda_idleL2l</Sound>
// </AudioEvent>

// oil tanker truck
// <AudioEvent id="NOD_FlameTank_IdleLoop" Volume="40%" VolumeShift="-5%" Limit="3" Priority="LOWEST" 
// Type="WORLD SHROUDED EVERYONE" Control="LOOP FADE_ON_KILL FADE_ON_START" MinRange="30" MaxRange="350" ReverbEffectLevel="100%"
// DryLevel="100%" SubmixSlider="SOUNDFX">
//     <PitchShift Low="-5" High="5" />
//     <Sound>NUFlame_idleLoopa</Sound>
//     <Sound>NUFlame_idleLoopb</Sound>
//     <Sound>NUFlame_idleLoopc</Sound>
//     <Sound>NUFlame_idleLoopd</Sound>
//     <Sound>NUFlame_idleLoope</Sound>
//     <Sound>NUFlame_idleLoopf</Sound>
//     <Sound>NUFlame_idleLoopg</Sound>
//     <Sound>NUFlame_idleLooph</Sound>
//     <Sound>NUFlame_idleLoopi</Sound>
// </AudioEvent>

//Player car
// <AudioEvent id="GDI_Slingshot_IdleLoop" Volume="40%" VolumeShift="-5%" Limit="3" Priority="LOWEST" 
    // Type="WORLD SHROUDED EVERYONE" Control="LOOP FADE_ON_KILL FADE_ON_START" MinRange="100" MaxRange="300" 
    // ReverbEffectLevel="100%" DryLevel="100%" SubmixSlider="SOUNDFX">
//     <PitchShift Low="-5" High="5"/>
//     <Sound>GUSling_idleLoopa</Sound>
//     <Sound>GUSling_idleLoopb</Sound>
//     <Sound>GUSling_idleLoopc</Sound>
//     <Sound>GUSling_idleLoopd</Sound>
//     <Sound>GUSling_idleLoope</Sound>
//     <Sound>GUSling_idleLoopf</Sound>
// </AudioEvent>