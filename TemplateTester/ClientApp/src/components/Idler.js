import * as React from 'react'
import { connect } from 'react-redux'
import { bindActionCreators } from 'redux'

import { BuyWorkerButton } from './BuyWorkerButton'

import {
	COST_WORKER,
	INCOME_FREQUENCY_MILLISECONDS,
	PRODUCTION_WORKER,
	actionCreators
} from '../store/Idler'

class SimpleIdler extends React.Component {
	constructor(props) {
		super(props)
		this.state = {
			incomeTimer: null
		}
	}

	componentDidMount() {
		window.document.title = 'Idler | Web UI'

		const incomeTimer = setInterval(
			this.props.collectIncome,
			INCOME_FREQUENCY_MILLISECONDS)
		this.setState({ incomeTimer })
	}

	componentWillUnmount() {
		if (this.state.incomeTimer) {
			clearInterval(this.state.incomeTimer)
		}
	}

	render() {
		return (
			<article>
				<h1>Idler</h1>
				<section>
					<h2>Resources: {this.props.numResource}</h2>
				</section>
				<section>
					<h2>Workers: {this.props.numWorkers}</h2>
				</section>
				<section>
					<button onClick={this.props.generateResource}>Generate Resource (+1)</button>
				</section>
				<section>
					<BuyWorkerButton
						buyWorker={this.props.buyWorker}
						cost={COST_WORKER}
						disabled={this.props.numResource < COST_WORKER}
						production={PRODUCTION_WORKER}
						/>
				</section>
			</article>
		)
	}
}

const Idler = connect(
	state => state.idler,
	dispatch => bindActionCreators(actionCreators, dispatch)
)(SimpleIdler)

export { Idler }
