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

import './Idler.css'

class SimpleIdler extends React.Component {
	constructor(props) {
		super(props)
		this.state = {
			progressTick: 0,
			progressTimer: null
		}
	}

	componentDidMount() {
		window.document.title = 'Idler | Web UI'

		const progressTimer = setInterval(this.onProgressTimerElapsed, INCOME_FREQUENCY_MILLISECONDS / 100)
		this.setState({ progressTimer })
	}

	componentWillUnmount() {
		if (this.state.progressTimer) {
			clearInterval(this.state.progressTimer)
		}
	}

	render() {
		return (
			<article>
				<h1>Idler</h1>
				<section>
					<h2>Resources: {this.props.numResource}</h2>
					{this.props.numWorkers > 0 &&
						<article>
							<label htmlFor="incomeProgress">Payday <em className="positive-number">+ {this.calculateProjectedPayday()}</em>:</label>
							<br />
							<progress id="incomeProgress" value={this.state.progressTick} max={100} />
						</article>}
				</section>
				<section>
					<h2>Workers: {this.props.numWorkers}</h2>
				</section>
				<section>
					<button className="generate-resource-button" onClick={this.props.generateResource}>Generate Resource <em className="positive-number">Prod=1</em></button>
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

	calculateProjectedPayday = () => this.props.numWorkers * PRODUCTION_WORKER

	onProgressTimerElapsed = () => {
		const { progressTick } = this.state

		if (progressTick < 100) {
			this.setState({ progressTick: progressTick + 1 })
			return
		}

		this.props.collectIncome()
		this.setState({ progressTick: 0 })
	}
}

const Idler = connect(
	state => state.idler,
	dispatch => bindActionCreators(actionCreators, dispatch)
)(SimpleIdler)

export { Idler }
