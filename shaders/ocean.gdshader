shader_type spatial;

void fragment() {
	ALBEDO = vec3(0.0, 0.4, 1.0);
	ROUGHNESS = 0.025;
}

//void light() {
//	// Called for every pixel for every light affecting the material.
//	// Uncomment to replace the default light processing function with this one.
//}
