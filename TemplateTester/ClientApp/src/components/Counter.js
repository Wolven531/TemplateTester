import React from 'react'
import { bindActionCreators } from 'redux'
import { connect } from 'react-redux'
import { actionCreators } from '../store/Counter'

class Counter extends React.Component {
	constructor(props){
		super(props)
		this.state = {
			alterAmount: 0
		}
	}

	componentDidMount() {
		window.document.title = 'Web UI | Counter'
	}

	render() {
		return <div>
			<h1>Counter</h1>
			<p>Current count: <strong>{this.props.count}</strong></p>
			<div>
				<label htmlFor="alterAmount">Alter amount</label>
				<input
					id="alterAmount"
					type="number"
					step={1}
					max={100} min={-100}
					value={this.state.alterAmount}
					onChange={this.handleChangeAlterAmount} />
				<button onClick={this.alterByAmount}>Alter by {this.state.alterAmount}</button>
			</div>
			<button onClick={this.props.decrement}>Decrement</button>
			<button onClick={this.props.increment}>Increment</button>
		</div>
	}

	alterByAmount = () => this.props.alterByAmount(this.state.alterAmount)

	handleChangeAlterAmount = (changeEvent) => {
		const alterAmount = parseInt(changeEvent.currentTarget.value, 10)
		this.setState({ alterAmount })
	}
}

export default connect(
	state => state.counter,
	dispatch => bindActionCreators(actionCreators, dispatch)
)(Counter)
