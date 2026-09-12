shader_type spatial;

// x-coordinate texture
uniform sampler2D alb_x : source_color;
uniform sampler2D nor_x;
uniform sampler2D rou_x;

// y-coordinate texture
uniform sampler2D alb_y : source_color;
uniform sampler2D nor_y;
uniform sampler2D rou_y;

// z-coordinate texture
uniform sampler2D alb_z : source_color;
uniform sampler2D nor_z;
uniform sampler2D rou_z;

vec3 triplanar(sampler2D txt_x, sampler2D txt_y, sampler2D txt_z, vec3 p, vec3 n) {
	float scale = 25.0;
	float m = 1.0;
	float delta = 0.25;

	// Sample the 3 texture maps
	vec4 x = texture(txt_x, p.yz / scale);
	vec4 y = texture(txt_y, p.xz / scale);
	vec4 z = texture(txt_z, p.xy / scale);
	
	// Get blend weights
	float bx = pow(max(abs(n.x) - delta, 0.0), m);
	float by = pow(max(abs(n.y) - delta, 0.0), m);
	float bz = pow(max(abs(n.z) - delta, 0.0), m);
	
	// Blend the values
	vec4 res = (bx * x + by * y + bz * z) / (bx + by + bz);
	
	return res.xyz;
}

void fragment() {
	vec3 pos = (INV_VIEW_MATRIX * vec4(VERTEX, 1.0)).xyz;
	vec3 nor = (INV_VIEW_MATRIX * vec4(NORMAL, 0.0)).xyz;
	
    ALBEDO = triplanar(alb_x, alb_y, alb_z, pos, nor);
	NORMAL_MAP = triplanar(nor_x, nor_y, nor_z, pos, nor);
	NORMAL_MAP_DEPTH = 2.0;
    ROUGHNESS = triplanar(rou_x, rou_y, rou_z, pos, nor).r;
}