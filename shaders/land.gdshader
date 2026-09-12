shader_type spatial;

uniform sampler2D txt_x : source_color;
uniform sampler2D txt_y : source_color;
uniform sampler2D txt_z : source_color;

// If the dot product of the normal is negative, then the
// surface is facing away from the plane and so we flip the direction
// to prevent mirroring of texture. 
vec4 triplanar(vec3 p, vec3 n) {
	// Sample the 3 texture maps
	vec4 x = texture(txt_x, p.yz / 50.0);
	vec4 y = texture(txt_y, p.xz / 50.0);
	vec4 z = texture(txt_z, p.xy / 10.0);
	
	// Blend the values
	n *= n;
	vec4 res = (n.x * x + n.y * y + n.z * z) / (n.x + n.y + n.z);
	
	return res;
}

void fragment() {
	vec4 pos = INV_VIEW_MATRIX * vec4(VERTEX, 1.0);
	vec4 norm = normalize(INV_VIEW_MATRIX * vec4(NORMAL, 0.0));
    ALBEDO = triplanar(pos.xyz, norm.xyz).xyz;
    ROUGHNESS = 1.0;
}