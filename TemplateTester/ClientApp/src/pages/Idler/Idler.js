import * as React from 'react'
import { connect } from 'react-redux'
import { bindActionCreators } from 'redux'

import { ResourceDisplay } from '../../components/ResourceDisplay'
import { UpgradesDisplay } from '../../components/UpgradesDisplay/UpgradesDisplay'
import { WorkerDisplay } from '../../components/WorkerDisplay'

import {
	INCOME_FREQUENCY_MILLISECONDS,
	actionCreators
} from '../../store/Idler'

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
				<ResourceDisplay
					generateResource={this.props.generateResource}
					numResource={this.props.numResource}
					numWorkers={this.props.numWorkers}
					percentToNextPayday={this.state.progressTick} />
				<WorkerDisplay
					buyWorker={this.props.buyWorker}
					numResource={this.props.numResource}
					numWorkers={this.props.numWorkers}
					workerCost={this.props.workerCost} />
				<UpgradesDisplay
					buyUpgrade={this.props.buyUpgrade}
					numResource={this.props.numResource} />
			</article>
		)
	}

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
