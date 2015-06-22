using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor( typeof ( Hayate ) ) ]
public class HayateEditor : Editor {
	
	public enum CalculationMethod{
		none, sine, cosine, perlin, precalculatedTexture, Audio
	}
	
	private bool useDebugFeatures;
	
	public override void OnInspectorGUI()
	{
		var hayate = target as Hayate;
		//DrawDefaultInspector ();
		
		EditorGUILayout.HelpBox("If the simulation is behaving strangely, make sure the particleSystem is set to 'Worldspace simulation'! If the simulation does not work in edit mode, push the 'pause' button twice.", MessageType.Info );
		
		if(hayate.UseTurbulence)
		{
			if(GUILayout.Button("Turbulence : ENABLED"))
				hayate.UseTurbulence = !hayate.UseTurbulence;
		}else{
			if(GUILayout.Button("Turbulence : DISABLED"))
				hayate.UseTurbulence = !hayate.UseTurbulence;
		}
		
		EditorGUI.BeginDisabledGroup(!hayate.UseTurbulence);
		
		if(hayate.drawTurbulenceField)
		{
			if(GUILayout.Button("Debugging features : ENABLED"))
				hayate.drawTurbulenceField = !hayate.drawTurbulenceField;
		}else{
			if(GUILayout.Button("Debugging features : DISABLED"))
				hayate.drawTurbulenceField = !hayate.drawTurbulenceField;
		}
		
		if(hayate.drawTurbulenceField)
		{
			EditorGUILayout.Space();
			
			EditorGUILayout.BeginHorizontal();
				EditorGUILayout.LabelField("Field Color");
				hayate.debugColor = EditorGUILayout.ColorField("", hayate.debugColor);
			EditorGUILayout.EndHorizontal();
			
			
			hayate.fieldSize = EditorGUILayout.Vector3Field("Field size:", hayate.fieldSize);
			hayate.stepSize = EditorGUILayout.Vector3Field("Step size:", hayate.stepSize);
			
			for(int i = 0; i < 5; i++)
				EditorGUILayout.Space();
		}
		
		
		EditorGUILayout.Space();
		EditorGUILayout.LabelField("Turbulence settings per axis (X/Y/Z):");
		
		EditorGUILayout.BeginHorizontal();
			hayate.UseCalculationMethodX = (Hayate.CalculationMethod) EditorGUILayout.EnumPopup( hayate.UseCalculationMethodX);
			hayate.UseCalculationMethodY = (Hayate.CalculationMethod) EditorGUILayout.EnumPopup( hayate.UseCalculationMethodY);
			hayate.UseCalculationMethodZ = (Hayate.CalculationMethod) EditorGUILayout.EnumPopup( hayate.UseCalculationMethodZ);
		EditorGUILayout.EndHorizontal();
		
		hayate.AssignTurbulenceTo = (Hayate.AssignTo) EditorGUILayout.EnumPopup("Assign to: ", hayate.AssignTurbulenceTo);
		hayate.UseRelativeOrAbsoluteValues = (Hayate.TurbulenceType) EditorGUILayout.EnumPopup("Simulation: ", hayate.UseRelativeOrAbsoluteValues);
		
		hayate.Amplitude = EditorGUILayout.Vector3Field("Amplitude", hayate.Amplitude);
		hayate.Frequency = EditorGUILayout.Vector3Field("Frequency", hayate.Frequency);
		hayate.Offset = EditorGUILayout.Vector3Field("Offset", hayate.Offset);
		hayate.OffsetSpeed = EditorGUILayout.Vector3Field("OffsetSpeed", hayate.OffsetSpeed);
		hayate.GlobalForce = EditorGUILayout.Vector3Field("Global force", hayate.GlobalForce);
		
		if(hayate.lockOffsetToEmitterPosition)
		{
			if(GUILayout.Button("Offset LOCKED to emitter position"))
				hayate.lockOffsetToEmitterPosition = !hayate.lockOffsetToEmitterPosition;
			
		}else{
			
			if(GUILayout.Button("Offset NOT LOCKED to emitter position"))
				hayate.lockOffsetToEmitterPosition = !hayate.lockOffsetToEmitterPosition;
		}
		
		EditorGUI.BeginDisabledGroup(hayate.lockOffsetToEmitterPosition);
		
		if(hayate.randomizeOffsetAtStart)
		{
			if(GUILayout.Button("Offset is being randomized on Start()"))
			{
				hayate.randomizeOffsetAtStart = !hayate.randomizeOffsetAtStart;
			}
		}else{
			if(GUILayout.Button("Offset is NOT being randomized on Start()"))
				hayate.randomizeOffsetAtStart = !hayate.randomizeOffsetAtStart;
		}
		
		EditorGUI.BeginDisabledGroup(!hayate.randomizeOffsetAtStart);
			hayate.randomOffsetRange = EditorGUILayout.Vector2Field("Random offset range: ", hayate.randomOffsetRange);
		EditorGUI.EndDisabledGroup();
		
		EditorGUI.EndDisabledGroup();
		EditorGUILayout.Space();
		
		
		if(	hayate.UseCalculationMethodX == Hayate.CalculationMethod.precalculatedTexture ||
			hayate.UseCalculationMethodY == Hayate.CalculationMethod.precalculatedTexture ||
			hayate.UseCalculationMethodZ == Hayate.CalculationMethod.precalculatedTexture )
		{
			EditorGUILayout.Space();
			EditorGUILayout.LabelField("Texture turbulence settings:");
			
			EditorGUILayout.Space();
			EditorGUILayout.BeginHorizontal();
			hayate.Turbulence = (Texture2D)EditorGUILayout.ObjectField("",hayate.Turbulence, typeof(Texture2D), true);
			EditorGUILayout.HelpBox("This texture uses different color channels per axis. Enable 'UseAlphaMask' to use the alpha channel as well. This will remove particles, that spawned where 'alpha = 0'.", MessageType.Info );
			EditorGUILayout.EndHorizontal();
			
			if(!hayate.Turbulence)
				EditorGUILayout.HelpBox("No Texture assigned! Precalculated turbulance only works with a Texture!", MessageType.Warning );
			
			EditorGUILayout.Space();
			
			EditorGUILayout.BeginHorizontal();
			
			if(hayate.useAlphaMask)
			{
				if(GUILayout.Button("Alpha mask: ENABLED"))
					hayate.useAlphaMask = !hayate.useAlphaMask;
				
			}else{
				if(GUILayout.Button("Alpha mask: DISABLED"))
					hayate.useAlphaMask = !hayate.useAlphaMask;
			}
			
			EditorGUI.BeginDisabledGroup(!hayate.useAlphaMask);
				hayate.threshold = EditorGUILayout.FloatField("Threshold:", hayate.threshold);
			EditorGUI.EndDisabledGroup();
			
			EditorGUILayout.EndHorizontal();
			
			if(GUILayout.Button("Update turbulence!") && hayate.Turbulence)
				hayate.UpdateTurbulence();
		}
		
		EditorGUILayout.Space();
		
		if(	hayate.UseCalculationMethodX == Hayate.CalculationMethod.Audio ||
			hayate.UseCalculationMethodY == Hayate.CalculationMethod.Audio ||
			hayate.UseCalculationMethodZ == Hayate.CalculationMethod.Audio )
		{
			if(!hayate.audioClip)
				EditorGUILayout.HelpBox("No AudioClip assigned! Audio turbulance only works with an audioClip assigned!", MessageType.Warning );
			
			EditorGUILayout.Space();
			EditorGUILayout.LabelField("Audio turbulence settings:");
			EditorGUILayout.Space();
			
			hayate.audioClip = (AudioClip)EditorGUILayout.ObjectField("",hayate.audioClip, typeof(AudioClip), true);
			EditorGUILayout.HelpBox("Make sure to select a sample far enough into the Track to not contain silence ('At sample'). Also the amount of samples should be inbetween 256 - 4096 and be a multiple of 2.", MessageType.Info );
			hayate.amountOfSamples = EditorGUILayout.IntField("Amount of samples: ", hayate.amountOfSamples);
			hayate.atSample = EditorGUILayout.IntField("At sample: ", hayate.atSample);
			
			if(GUILayout.Button("Update turbulence!") && hayate.audioClip)
				hayate.calculateAudioTurbulence();
			
			for(int i = 0; i < 5; i++)
				EditorGUILayout.Space();
		}
		
		EditorGUILayout.Space();
		
		EditorGUILayout.Space();
		EditorGUILayout.LabelField("Collision settings:");
				
		if(hayate.burstOnCollision)
		{
			if(GUILayout.Button("Emit particles on collision: ENABLED"))
				hayate.burstOnCollision = !hayate.burstOnCollision;
			
		}else{
			if(GUILayout.Button("Emit particles on collision: DISABLED"))
				hayate.burstOnCollision = !hayate.burstOnCollision;
		}
		
		if(hayate.burstOnCollision)
		{
			hayate.burstNum = EditorGUILayout.IntField("Emit particles: ", hayate.burstNum);
		
			for(int i = 0; i < 5; i++)
				EditorGUILayout.Space();
		}
			
		EditorGUILayout.Space();
		EditorGUILayout.LabelField("Transform particle options:");
		
		if(hayate.useTransformParticle)
		{
			if(GUILayout.Button("Transform particles: ENABLED"))
				hayate.useTransformParticle = !hayate.useTransformParticle;
			
		}else{
			if(GUILayout.Button("Transform particles: DISABLED"))
				hayate.useTransformParticle = !hayate.useTransformParticle;
		}
		
		if(hayate.useTransformParticle)
		{
			hayate.transformParticle = (GameObject)EditorGUILayout.ObjectField("",hayate.transformParticle, typeof(GameObject), true);
			
			EditorGUILayout.LabelField("What to do with Transform after particle death:");
			
			EditorGUILayout.BeginHorizontal();
			
				if(hayate.detachTransformParticleAfterParticleDeath)
				{
					if(GUILayout.Button("Detach"))
						hayate.detachTransformParticleAfterParticleDeath = !hayate.detachTransformParticleAfterParticleDeath;
				
				}else{
					if(GUILayout.Button("Delete"))
						hayate.detachTransformParticleAfterParticleDeath = !hayate.detachTransformParticleAfterParticleDeath;
				}
				
				EditorGUI.BeginDisabledGroup(!hayate.detachTransformParticleAfterParticleDeath);
					hayate.detachedObjectDestructionTimeAfter = EditorGUILayout.FloatField("Time until death:", hayate.detachedObjectDestructionTimeAfter);
				EditorGUI.EndDisabledGroup();
				
			EditorGUILayout.EndHorizontal();
			
			if(hayate.transformParticleLookTowardsFlightDirection)
			{
				if(GUILayout.Button("Transform particle looking at flight direction."))
					hayate.transformParticleLookTowardsFlightDirection = !hayate.transformParticleLookTowardsFlightDirection;
			
			}else{
				if(GUILayout.Button("Transform particle rotation is not being altered."))
					hayate.transformParticleLookTowardsFlightDirection = !hayate.transformParticleLookTowardsFlightDirection;
			}
			
			for(int i = 0; i < 5; i++)
				EditorGUILayout.Space();
		}
		
		EditorGUILayout.Space();
		EditorGUILayout.LabelField("Follow point:");
		
		if(hayate.followPoint)
		{
			if(GUILayout.Button("Following point: "+ hayate.followPosition.ToString()))
				hayate.followPoint = !hayate.followPoint;
			
		}else{
			if(GUILayout.Button("Follow point DISABLED"))
				hayate.followPoint = !hayate.followPoint;
		}
		
		if(hayate.followPoint)
		{
			hayate.followTransform = (Transform)EditorGUILayout.ObjectField("Follow Transform: ",hayate.followTransform, typeof(Transform), true);
			hayate.followPosition = EditorGUILayout.Vector3Field("Follow Point: ", hayate.followPosition);
			hayate.followStrength = EditorGUILayout.FloatField("Strength: ", hayate.followStrength);
			
			for(int i = 0; i < 5; i++)
				EditorGUILayout.Space();
		}
		
		EditorGUILayout.Space();
		EditorGUILayout.LabelField("Mesh target options:");
		
		if(hayate.moveToMesh)
		{
			if(GUILayout.Button("Moving to mesh"))
				hayate.moveToMesh = !hayate.moveToMesh;
			
		}else{
			if(GUILayout.Button("Mesh target DISABLED"))
				hayate.moveToMesh = !hayate.moveToMesh;
		}
		
		if(hayate.moveToMesh)
		{
			EditorGUILayout.BeginHorizontal();
				hayate.meshFollow = (Hayate.MeshFollow) EditorGUILayout.EnumPopup( hayate.meshFollow);
				hayate.meshTarget = (GameObject)EditorGUILayout.ObjectField("",hayate.meshTarget, typeof(GameObject), true);
			EditorGUILayout.EndHorizontal();
			
			hayate.particleSpeedToMesh = EditorGUILayout.FloatField("Speed: ", hayate.particleSpeedToMesh);
			
			EditorGUILayout.Space();
			EditorGUILayout.LabelField("Subdivision options:");
			hayate.centerDivision = (Hayate.DivisionType) EditorGUILayout.EnumPopup( hayate.centerDivision);
			hayate.smallestTriangle = EditorGUILayout.FloatField("Smallest triangle: ", hayate.smallestTriangle);
			
			EditorGUILayout.BeginHorizontal();
			
			if(GUILayout.Button("Get mesh info"))
				hayate.retrieveMeshInfo();
				
			if(GUILayout.Button("Subdivide"))
				hayate.UpdateMeshCoordinates();
						
			if(GUILayout.Button("Reset"))
				hayate.rstMesh();
			
			EditorGUILayout.EndHorizontal();
			
			for(int i = 0; i < 5; i++)
				EditorGUILayout.Space();
		}
		
		EditorGUILayout.Space();
		EditorGUILayout.LabelField("Threading options:");
		
		if(hayate.useThreading)
		{
			if(GUILayout.Button("Threading ENABLED"))
				hayate.useThreading = !hayate.useThreading;
			
		}else{
			if(GUILayout.Button("Threading DISABLED"))
				hayate.useThreading = !hayate.useThreading;
		}
		
		if(hayate.useThreading)
		{
			if(hayate.safeMode)
			{
				if(GUILayout.Button("Safe mode ENABLED"))
					hayate.safeMode = !hayate.safeMode;
				
			}else{
				if(GUILayout.Button("Safe mode DISABLED"))
					hayate.safeMode = !hayate.safeMode;
			}
			
			hayate.numThreads = EditorGUILayout.IntField("Threads: ", hayate.numThreads);
		}
		
		EditorGUI.EndDisabledGroup();
	}
}
