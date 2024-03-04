import React, { useRef, useEffect } from 'react';
import Chart from 'chart.js/auto';
import { Pie } from 'react-chartjs-2';
import './style.css'


const PieChart = ({ data }) => {
  const chartRef = useRef(null);

  useEffect(() => {
    const chartInstance = chartRef.current?.chartInstance;
    if (chartInstance) {
      chartInstance.destroy();
    }
  }, [data]);

  return (
    <div style={{ width: '100%', backgroundColor: 'lightblue' }}>

        <div className="row">
          <div className="col-md-12 mb-4">
            <div className="card p-4 custom-bg-color4">
              <div className="card-body">
                <div style={{ display: 'flex', justifyContent: 'center', alignItems: 'center', width: '100%', height: '100%' }}>
                  <div style={{ padding: '20px', backgroundColor: ' #F5F5DC' }}>
                    <h2>Transactions</h2>
                    <Pie ref={chartRef} data={data} />
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>

    </div>

  );
};

export default PieChart;
