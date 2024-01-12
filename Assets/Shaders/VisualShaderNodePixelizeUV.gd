@tool
extends VisualShaderNodeCustom
class_name VisualShaderNodePixelizeUV


func _get_name():
	return "PixelizeUV"


func _get_category():
	return "MyShaderNodes"


func _get_description():
	return "Quantize coordinates based on pixel size"


func _init():
	set_input_port_default_value(1, Vector2(0.1, 0.1))


func _get_return_icon_type():
	return VisualShaderNode.PORT_TYPE_VECTOR_2D


func _get_input_port_count():
	return 2


func _get_input_port_name(port):
	match port:
		0:
			return "uv"
		1:
			return "pixelSize"


func _get_input_port_type(port):
	match port:
		0:
			return VisualShaderNode.PORT_TYPE_VECTOR_2D
		1:
			return VisualShaderNode.PORT_TYPE_VECTOR_2D


func _get_output_port_count():
	return 1


func _get_output_port_name(_port):
	return "result"


func _get_output_port_type(_port):
	return VisualShaderNode.PORT_TYPE_VECTOR_2D


func _get_global_code(_mode):
	return """
		float floatPixelate1(float f, float amount) {
			return floor(f * amount) / amount;
		}

		vec2 pixelate_uv(vec2 P, vec2 pixel_size) {
			float x_amount = 1.0 / pixel_size.x;
			float y_amount = 1.0 / pixel_size.y;
			return vec2(floatPixelate1(P.x, x_amount), floatPixelate1(P.y, y_amount));
		}
	"""


func _get_code(input_vars, output_vars, _mode, _type):
	return output_vars[0] + " = pixelate_uv(%s.xy, %s.xy);" % [input_vars[0], input_vars[1]]
