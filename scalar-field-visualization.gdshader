shader_type spatial;

uniform vec3 position; 

void fragment() {
	ALBEDO = vec3(position.x * 0.05, position.y * 0.05, position.z * 0.05);
}

//void light() {
//	// Called for every pixel for every light affecting the material.
//	// Uncomment to replace the default light processing function with this one.
//}
