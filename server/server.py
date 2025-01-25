from flask import Flask, request, jsonify

app = Flask(__name__)

# 用于存储心率数据的列表
heart_rate_data = 0

@app.route('/report', methods=['GET'])
def report_heart_rate():
    heart_rate = request.args.get('heart_rate', type=int)
    
    if heart_rate is None:
        return jsonify({'error': 'Missing heart_rate'}), 400
    
    global heart_rate_data
    heart_rate_data = heart_rate
    return jsonify({'message': 'Heart rate reported successfully','rate':heart_rate_data})

@app.route('/read', methods=['GET'])
def read_heart_rates():
    return jsonify(heart_rate_data)

if __name__ == '__main__':
    app.run(host='0.0.0.0', port=5000)
